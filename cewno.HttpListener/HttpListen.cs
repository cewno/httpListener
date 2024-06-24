namespace cewno.HttpListener;

public abstract class HttpListen(Action<Stream> work)
{
	public abstract void Start();
	public abstract void Stop();

	protected void intoListen(Stream stream)
	{
		work.Invoke(stream);
	}
}