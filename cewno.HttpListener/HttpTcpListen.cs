using System.Net;
using System.Net.Sockets;

namespace cewno.HttpListener;

public abstract class AbstractHttpListen(IPEndPoint address, Action<Stream> work) : HttpListen(work)
{
	private readonly IPEndPoint _address = address;
	private readonly TcpListener _listener = new TcpListener(address);
	private bool _nostop = true;

	public override void Stop()
	{
		_nostop = false;
	}

	protected void Listen()
	{
		_listener.Start();
		TcpClient client;
		while (_nostop)
		{
			client = _listener.AcceptTcpClient();
			client.GetStream();
			intoListen(client.GetStream());
		}
	}
}

public class HttpTaskListen : AbstractHttpListen
{
	private readonly Task _listenTask;

	public HttpTaskListen(IPEndPoint address, Action<Stream> work) : base(address, work)
	{
		_listenTask = new Task(Listen);
	}

	public override void Start()
	{
		_listenTask.Start();
	}
}

public class HttpThreadListen : AbstractHttpListen
{
	private readonly Thread _listenThread;

	public HttpThreadListen(IPEndPoint address, Action<Stream> work) : base(address, work)
	{
		_listenThread = new Thread(Listen);
	}

	public override void Start()
	{
		_listenThread.Start();
	}
}