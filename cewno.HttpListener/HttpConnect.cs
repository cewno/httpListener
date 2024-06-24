namespace cewno.HttpListener;

/// <summary>
///     在http1.1长连接、HTTP2、QUIC、HTTP3时，一个连接会被复用多次
/// </summary>
public interface HttpConnect
{
	/// <summary>
	///     HTTP协议版本
	/// </summary>
	public byte Version { get; }

	/// <summary>
	///     该连接使用TCP，否则使用QUIC
	/// </summary>
	public bool IsTcp { get; }

	/// <summary>
	///     该连接是否是安全连接，在Version = HTTP2 over QUIC 或 Version = HTTP3时，该值总是为true
	/// </summary>
	public bool IsSecurity { get; }

	public IHttpRoute Route { get; }
}