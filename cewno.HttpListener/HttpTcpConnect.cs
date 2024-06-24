using System.Net.Security;
using System.Text;
using HttpServer.exception;

namespace cewno.HttpListener;

public abstract class HttpTcpConnectAbstract(Stream stream, IHttpRoute route) : HttpConnect
{
	protected readonly BufferReadAndWriteStream Stream = new BufferReadAndWriteStream(stream);
	protected ulong MaxHeaderLength = 10240;
	protected int MaxPathLength = 20;
	public byte Version { set; get; }

	public bool IsTcp => true;
	public abstract bool IsSecurity { get; }

	public IHttpRoute Route
	{
		get => route;
		set => route = value;
	}

	protected void Listen()
	{
		HttpContext hx = new HttpContext(this);
		hx.method = ParseMethod();
		string path = ParesPath();
		Version = ParesVersion();
		hx.Version = Version;
		hx.RequestHeader = ParesHeader();
		/////////待办 ：  进行SslStream SNI 扩展中的域名与 HTTP Host header 的匹配判断
		if (hx.RequestHeader.TryGetValue("Host", out string? host))
		{
			hx.uri = new Uri($"http{(IsSecurity ? "s" : "")}://{host}{path}");
		}
		else
		{
			Stream.pWrite(Encoding.UTF8.GetBytes("HTTP/1.1 400 Bad Request\r\n\r\n"));
			Stream.Close();
		}

		Console.WriteLine(hx.method + hx.uri.ToString());
		hx.Stream = Stream;
		Stream.wlock = true;
		route.Route(hx);
	}

	#region ParseMethod

	private byte ParseMethod()
	{
		byte[] buffer = new byte[4];
		if (Stream.Read(buffer, 0, 4) < 4) throw new HttpServerExcepiton("Not enough data");
		if (buffer[2] == 'I')
		{
			Stream.pWrite(Encoding.UTF8.GetBytes("HTTP/1.1 505 HTTP Version Not Supported\r\n\r\n"));
			Stream.Close();
			Version = HttpVersion.HTTP2;
		}

		switch (buffer[0])
		{
			// GET
			case (byte)'G':
				if (buffer[1] == 'E' && buffer[2] == 'T') return HttpMethod.GET;
				break;
			//P**
			case (byte)'P':
				switch (buffer[1])
				{
					//POST
					case (byte)'O':
						if (buffer[2] == 'S' && buffer[3] == 'T')
						{
							//抛弃空格
							Stream.Readskipbyte();
							return HttpMethod.POST;
						}

						break;
					//PUT
					case (byte)'U':
						if (buffer[2] == 'T') return HttpMethod.PUT;
						break;
					//PATCH
					case (byte)'A':
						if (buffer[2] == 'T' && buffer[3] == 'C' && Stream.ReadByte() == 'H')
						{
							//抛弃空格
							Stream.Readskipbyte();
							return HttpMethod.PATCH;
						}

						break;
				}

				break;
			//OPTIONS
			case (byte)'O':
				if (buffer[1] == 'P' && buffer[2] == 'T' && buffer[3] == 'I' && Stream.ReadByte() == 'O' &&
				    Stream.ReadByte() == 'N' && Stream.ReadByte() == 'S')
				{
					//抛弃空格
					Stream.Readskipbyte();
					return HttpMethod.OPTIONS;
				}

				break;
			//DELETE
			case (byte)'D':
				if (buffer[1] == 'E' && buffer[2] == 'L' && buffer[3] == 'E' && Stream.ReadByte() == 'T' &&
				    Stream.ReadByte() == 'E')
				{
					//抛弃空格
					Stream.Readskipbyte();
					return HttpMethod.DELETE;
				}

				break;
			//TRACE
			case (byte)'T':
				if (buffer[1] == 'R' && buffer[2] == 'A' && buffer[3] == 'C' && Stream.ReadByte() == 'E')
				{
					//抛弃空格
					Stream.Readskipbyte();
					return HttpMethod.TRACE;
				}

				break;
			//HEAD
			case (byte)'H':
				if (buffer[1] == 'E' && buffer[2] == 'A' && buffer[3] == 'D')
				{
					//抛弃空格
					Stream.Readskipbyte();
					return HttpMethod.HEAD;
				}

				break;
			//CONNECT
			case (byte)'C':
				if (buffer[1] == 'O' && buffer[2] == 'N' && buffer[3] == 'N' && Stream.ReadByte() == 'E' &&
				    Stream.ReadByte() == 'C' && Stream.ReadByte() == 'T')
				{
					//抛弃空格
					Stream.Readskipbyte();
					return HttpMethod.CONNECT;
				}

				break;
		}

		throw new UnknownMethodOrOtherProtocol();
	}

	#endregion

	#region ParesUrl

	protected string ParesPath()
	{
		List<byte> data = new List<byte>();
		int at;
		while (true)
		{
			if (MaxPathLength > 0 && data.Count >= MaxPathLength)
			{
				Stream.pWrite(Encoding.UTF8.GetBytes("HTTP/1.1 414 URI Too Long\r\n\r\n"));
				Stream.Close();
				throw new HttpServerExcepiton("Wrong path format");
			}

			at = Stream.ReadByte();
			if (at == ' ') break;
			data.Add((byte)at);
		}

		return Encoding.UTF8.GetString(data.ToArray(), 0, data.Count);
	}

	#endregion

	#region paresVersion

	private byte ParesVersion()
	{
		byte[] buffer = new byte[1];
		Stream.Read(buffer, 0, 1);
		if (buffer[0] == '\r')
		{
		}
		else if (buffer[0] == 'H')
		{
			buffer = new byte[9];
			Stream.Read(buffer, 0, 9);
			if ((buffer[0], buffer[1], buffer[2], buffer[3], buffer[4], buffer[5], buffer[6], buffer[7], buffer[8]) is (
			    (byte)'T', (byte)'T', (byte)'P', (byte)'/', (byte)'1', (byte)'.', (byte)'1', (byte)'\r', (byte)'\n'
			    )) //HTTP/1.1
				return HttpVersion.HTTP_1_1;
		}

		Stream.Write(Encoding.UTF8.GetBytes("HTTP/1 505 HTTP Version Not Supported\r\n\r\n"));
		Stream.Close();
		throw new UnknownMethodOrOtherProtocol();
	}

	#endregion

	#region paresHeader

	private HttpRequestHeader ParesHeader()
	{
		HttpRequestHeader hrh = new HttpRequestHeader();
		byte[] buff = new byte[1];
		while (true)
		{
			List<byte> data = new List<byte>();
			string key;
			string value;
			while (true)
			{
				if (Stream.Read(buff, 0, 1) < 1) goto down;
				if (buff[0] == '\r')
				{
					//抛弃‘\n’
					Stream.Read(buff, 0, 1);
					goto down;
				}

				if (buff[0] == ':')
				{
					//抛弃空格
					Stream.Read(buff, 0, 1);
					break;
				}

				data.Add(buff[0]);
			}

			key = Encoding.UTF8.GetString(data.ToArray(), 0, data.Count);
			data.Clear();
			while (true)
			{
				Stream.Read(buff, 0, 1);
				if (buff[0] == '\r')
				{
					//抛弃‘\n’
					Stream.Read(buff, 0, 1);
					break;
				}

				data.Add(buff[0]);
			}

			value = Encoding.UTF8.GetString(data.ToArray(), 0, data.Count);
			hrh[key] = value;
			continue;
			down:
			break;
		}

		return hrh;
	}

	#endregion
}

public class HttpTcpTaskConnect : HttpTcpConnectAbstract
{
	private readonly Task _listenerTask;

	public HttpTcpTaskConnect(Stream stream, IHttpRoute route) : base(stream, route)
	{
		IsSecurity = false;
		_listenerTask = new Task(Listen);
		_listenerTask.Start();
	}

	public HttpTcpTaskConnect(SslStream sslStream, IHttpRoute route) : base(sslStream, route)
	{
		IsSecurity = true;
		_listenerTask = new Task(Listen);
		_listenerTask.Start();
	}

	public HttpTcpTaskConnect(Stream stream, ushort buffSize, ushort MaxStringLength, IHttpRoute route) : base(stream,
		route)
	{
		IsSecurity = false;
		stream.Read(new byte[1]);
		_listenerTask = new Task(Listen);
		_listenerTask.Start();
	}

	public HttpTcpTaskConnect(SslStream sslStream, ushort buffSize, ushort MaxStringLength, IHttpRoute route) : base(
		sslStream, route)
	{
		IsSecurity = true;
		_listenerTask = new Task(Listen);
		_listenerTask.Start();
	}

	public override bool IsSecurity { get; }
}