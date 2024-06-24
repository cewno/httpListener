using System.Buffers;
using System.Text;
using HttpServer.exception;

namespace cewno.HttpListener;

public class BufferReadAndWriteStream : Stream
{
	//"\r\n"
	private static readonly byte[] crlf = Encoding.UTF8.GetBytes("\r\n");

	private static readonly byte[] chunkedmsg = Encoding.UTF8.GetBytes("0\r\n");
	private readonly byte[] _readBuffer;
	private readonly int _readBufferSize;
	private readonly Stream _streamImplementation;
	private readonly byte[] _writeBuffer;
	private readonly int _writeBufferSize;
	private int _readBuffedSize;
	private int _readBufferPos;
	private int _writeBuffedSize;

	protected internal bool chunked = false;

	protected internal bool wlock = false;

	public BufferReadAndWriteStream(Stream streamImplementation, int readBufferSize, int writeBufferSize)
	{
		_readBuffer = new byte[readBufferSize];
		_writeBuffer = new byte[writeBufferSize];
		_readBufferSize = readBufferSize;
		_writeBufferSize = writeBufferSize;
		_streamImplementation = streamImplementation;
	}

	public BufferReadAndWriteStream(Stream streamImplementation, int readAndwriteBufferSize) : this(
		streamImplementation, readAndwriteBufferSize, readAndwriteBufferSize)
	{
	}

	public BufferReadAndWriteStream(Stream streamImplementation) : this(streamImplementation, 4096, 4096)
	{
	}


	public override bool CanRead => _streamImplementation.CanRead;

	public override bool CanSeek => _streamImplementation.CanSeek;

	public override bool CanWrite => _streamImplementation.CanWrite;

	public override long Length => _streamImplementation.Length;

	public override long Position
	{
		get => _streamImplementation.Position;
		set => _streamImplementation.Position = value;
	}

	private void ReadFromBuff(byte[] buffer, int offset, int count)
	{
		Array.Copy(_readBuffer, _readBufferPos, buffer, offset, count);
		_readBuffedSize -= count;
		_readBufferPos += count;
	}


	public override int Read(byte[] buffer, int offset, int count)
	{
		if (count == 0) return 0;
		st:
		if (count > _readBuffedSize)
		{
			if (_readBuffedSize == 0)
			{
				//比最大大小大
				if (count >= _readBufferSize) return _streamImplementation.Read(buffer, offset, count);
				//无数据
				if (buff() == 0) return 0;
				goto st;
			}

			int ls = Math.Min(count, _readBuffedSize);
			ReadFromBuff(buffer, offset, ls);
			offset += ls;
			count -= ls;
			return ls + Read(buffer, offset, count);
		}

		ReadFromBuff(buffer, offset, count);
		return count;
	}

	protected internal void pWrite(byte[] buffer, int offset, int count)
	{
		if (count == 0) return;

		if (count > _writeBufferSize - _writeBuffedSize)
		{
			WriteFromBuff();
			_streamImplementation.Write(buffer, offset, count);
			return;
		}

		Array.Copy(buffer, offset, _writeBuffer, _writeBuffedSize, count);
		_writeBuffedSize += count;
	}

	protected internal void pWrite(ReadOnlySpan<byte> buffer)
	{
		byte[] numArray = ArrayPool<byte>.Shared.Rent(buffer.Length);
		try
		{
			buffer.CopyTo((Span<byte>)numArray);
			pWrite(numArray, 0, buffer.Length);
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(numArray);
		}
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (wlock) throw new HttpServerExcepiton("No response status code or headers were sent");
		if (count == 0) return;

		if (chunked)
		{
			if (count > _writeBufferSize - _writeBuffedSize)
			{
				_streamImplementation.Write(Encoding.UTF8.GetBytes((_writeBuffedSize + count).ToString("X")));
				_streamImplementation.Write(crlf);
				WriteFromBuff();
				_streamImplementation.Write(buffer, offset, count);
				_streamImplementation.Write(crlf);
				return;
			}

			Array.Copy(buffer, offset, _writeBuffer, _writeBuffedSize, count);
			_writeBuffedSize += count;
		}
		else
		{
			if (count > _writeBufferSize - _writeBuffedSize)
			{
				WriteFromBuff();
				_streamImplementation.Write(buffer, offset, count);
				return;
			}

			Array.Copy(buffer, offset, _writeBuffer, _writeBuffedSize, count);
			_writeBuffedSize += count;
		}
	}

	public int Readskip(int l)
	{
		byte[] bytes = ArrayPool<byte>.Shared.Rent(l);
		try
		{
			return Read(bytes, 0, l);
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(bytes);
		}
	}

	public void Readskipbyte()
	{
		byte[] bytes = ArrayPool<byte>.Shared.Rent(1);
		try
		{
			Read(bytes, 0, 1);
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(bytes);
		}
	}

	private void WriteFromBuff()
	{
		_streamImplementation.Write(_writeBuffer, 0, _writeBuffedSize);
		_writeBuffedSize = 0;
	}

	public override void Flush()
	{
		if (_writeBuffedSize == 0) goto l;
		if (chunked)
		{
			_streamImplementation.Write(Encoding.UTF8.GetBytes(_writeBuffedSize.ToString("X")));
			_streamImplementation.Write(crlf);
			WriteFromBuff();
			_streamImplementation.Write(crlf);
		}
		else
		{
			WriteFromBuff();
		}

		l:
		_streamImplementation.Flush();
	}

	private int buff()
	{
		_readBufferPos = 0;
		return _readBuffedSize = _streamImplementation.Read(_readBuffer, 0, _readBufferSize);
	}

	public override void Close()
	{
		Flush();
		if (chunked) _streamImplementation.Write(chunkedmsg);
		_streamImplementation.Write(crlf);
		_streamImplementation.Close();
		base.Close();
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		return _streamImplementation.Seek(offset, origin);
	}

	public override void SetLength(long value)
	{
		_streamImplementation.SetLength(value);
	}
}