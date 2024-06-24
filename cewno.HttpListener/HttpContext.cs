using System.Text;
using HttpServer.exception;

namespace cewno.HttpListener;

public sealed record HttpContext : IDisposable, IAsyncDisposable
{
	// ": "
	private static readonly byte[] hf = [58, 20];

	//"\r\n"
	private static readonly byte[] crlf = { 13, 10 };

	public readonly HttpConnect hc;

	private bool SendResponsHeadersED;

	private bool SendResponsStatusCodeED;

	public HttpContext(HttpConnect hc)
	{
		this.hc = hc;
		ResponseHeader = new HttpResponseHeader();
	}

	public BufferReadAndWriteStream Stream { protected internal set; get; }

	public byte method { protected internal set; get; }

	public Uri uri { protected internal set; get; }

	public byte Version { protected internal set; get; }

	public HttpRequestHeader RequestHeader { protected internal set; get; }

	public HttpResponseHeader ResponseHeader { protected internal set; get; }

	public async ValueTask DisposeAsync()
	{
		await Stream.DisposeAsync();
	}

	public void Dispose()
	{
		Stream.Close();
	}

	/// <summary>
	///     发送状态码
	/// </summary>
	/// <param name="code">状态码</param>
	public void SendResponsStatusCode(ushort code)
	{
		if (SendResponsStatusCodeED) return;
		switch (Version)
		{
			case HttpVersion.HTTP_1_1:
				Stream.pWrite("HTTP/1.1 "u8.ToArray());
				Stream.pWrite(HttpStatusCodes.GetCodeMsg(code));
				Stream.pWrite(crlf, 0, 2);
				break;
		}

		SendResponsStatusCodeED = true;
	}

	/// <summary>
	///     发送响应头，并确定响应体以固定长度响应
	/// </summary>
	/// <param name="responsBodyLen"></param>
	public void SendResponsHeaders(int responsBodyLen)
	{
		if (SendResponsHeadersED) return;
		if (!SendResponsStatusCodeED) throw new HttpServerExcepiton("No response headers were sent");
		switch (Version)
		{
			case HttpVersion.HTTP_1_1:
				ResponseHeader.put("Content-Length", responsBodyLen.ToString());
				foreach (KeyValuePair<string, IList<string>> keyValuePair in ResponseHeader)
				foreach (string s in keyValuePair.Value)
				{
					Stream.pWrite(Encoding.UTF8.GetBytes(keyValuePair.Key));
					Stream.pWrite(hf, 0, 1);
					Stream.pWrite(Encoding.UTF8.GetBytes(s));
					Stream.pWrite(crlf, 0, 2);
				}

				Stream.pWrite(crlf, 0, 2);
				break;
		}

		SendResponsHeadersED = true;
		Stream.wlock = false;
	}

	/// <summary>
	///     发送响应头，并确定响应体以动态长度响应
	/// </summary>
	public void SendResponsHeaders()
	{
		if (SendResponsHeadersED) return;
		if (!SendResponsStatusCodeED) throw new HttpServerExcepiton("No response headers were sent");
		switch (Version)
		{
			case HttpVersion.HTTP_1_1:
				ResponseHeader.Remove("Content-Length");
				ResponseHeader.put("Transfer-Encoding", "chunked");
				foreach (KeyValuePair<string, IList<string>> keyValuePair in ResponseHeader)
				foreach (string s in keyValuePair.Value)
				{
					Stream.pWrite(Encoding.UTF8.GetBytes(keyValuePair.Key));
					Stream.pWrite(hf, 0, 1);
					Stream.pWrite(Encoding.UTF8.GetBytes(s));
					Stream.pWrite(crlf, 0, 2);
				}

				Stream.pWrite(crlf, 0, 2);
				Stream.Flush();
				Stream.chunked = true;
				break;
		}

		SendResponsHeadersED = true;

		Stream.wlock = false;
	}


	public void Close()
	{
		Dispose();
	}

	public void Deconstruct(out HttpConnect hc)
	{
		hc = this.hc;
	}
}