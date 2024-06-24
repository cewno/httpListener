namespace cewno.HttpListener;

/// <summary>
///     http路由接口
/// </summary>
public interface IHttpRoute
{
	/// <summary>
	///     路由
	/// </summary>
	/// <param name="host">请求主机</param>
	/// <param name="path">请求路径</param>
	/// <param name="context">
	///     <see cref="HttpContent" />
	/// </param>
	public void Route(HttpContext context);
}