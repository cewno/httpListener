namespace cewno.HttpListener;

public sealed class HttpMethod
{
	public static readonly byte UNKNOWN = 0;
	public static readonly byte GET = 1;
	public static readonly byte POST = 2;
	public static readonly byte PUT = 3;
	public static readonly byte DELETE = 4;
	public static readonly byte HEAD = 5;
	public static readonly byte OPTIONS = 6;
	public static readonly byte TRACE = 7;
	public static readonly byte PATCH = 8;
	public static readonly byte CONNECT = 9;
}