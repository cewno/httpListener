namespace cewno.HttpListener;

public class HttpStream : BufferReadAndWriteStream
{
	public HttpStream(Stream streamImplementation, int readBufferSize, int writeBufferSize) : base(streamImplementation,
		readBufferSize, writeBufferSize)
	{
	}

	public HttpStream(Stream streamImplementation, int readAndwriteBufferSize) : base(streamImplementation,
		readAndwriteBufferSize)
	{
	}

	public HttpStream(Stream streamImplementation) : base(streamImplementation)
	{
	}
}