using System.Text;

namespace cewno.HttpListener;

public class HttpResponseHeader : Dictionary<string, IList<string>>
{
	/// <summary>
	///     添加一个响应头
	/// </summary>
	/// <param name="key">头名称</param>
	/// <param name="value">值</param>
	public void addHeader(string key, string value)
	{
		if (!ContainsKey(key))
			Add(key, new List<string>());
		this[key].Add(value);
	}

	/// <summary>
	///     覆写响应头
	/// </summary>
	/// <param name="key">被覆写的头key</param>
	/// <param name="value">要覆写的值</param>
	public void put(string key, string value)
	{
		this[key] = new List<string> { value };
	}

	/// <summary>
	///     覆写响应头
	/// </summary>
	/// <param name="key">被覆写的头key</param>
	/// <param name="value">要覆写的值</param>
	public void put(string key, List<string> value)
	{
		this[key] = value;
	}

	/// <summary>
	///     添加一组响应头
	/// </summary>
	/// <param name="key">头名称</param>
	/// <param name="values">一组值</param>
	public void addHeader(string key, IList<string> values)
	{
		if (!ContainsKey(key)) this[key] = new List<string>();
		foreach (string value in values) this[key].Add(value);
	}

	/// <summary>
	///     添加一个Set-Cookie头，告诉客户端添加一个Cookie
	/// </summary>
	/// <param name="id">Cookie id</param>
	/// <param name="value">Cookie 值</param>
	/// <param name="expires">Cookie 过期时间</param>
	/// <param name="sameSite">控制 Cookie 是否随跨站请求一起发送，<see cref="CookieTagSameSite" /></param>
	/// <param name="domain">
	///     指定 cookie 可以送达的主机。
	///     只能将值设置为当前域名或更高级别的域名（除非是公共后缀）。设置域名将会使 cookie 对指定的域名及其所有子域名可用。
	///     若缺省，则此属性默认为当前文档 URL 的主机（不包括子域名）。
	///     多个主机/域名的值是不被允许的，但如果指定了一个域名，则其子域名也总会被包含。
	///     来自 <seealso href="https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Headers/Set-Cookie">MDN</seealso>
	///     归属于
	///     <seealso href="https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Headers/Set-Cookie/contributors.txt">Mozilla 贡献者</seealso>
	///     的署名和版权许可在 <seealso href="https://creativecommons.org/licenses/by-sa/2.5/deed.zh">知识共享 署名—相同方式共享 2.5 许可下提供</seealso>
	/// </param>
	/// <param name="path"></param>
	/// <param name="httpOnly">若为true，不能通过客户端的JavaScript脚本访问到该 Cookie</param>
	/// <param name="partitioned">
	///     表示应使用分区存储来存储 cookie。有关更多详细信息，请参见
	///     <seealso href="https://developer.mozilla.org/en-US/docs/Web/Privacy/Privacy_sandbox/Partitioned_cookies">
	///         具有独立分区状态的
	///         Cookie（CHIPS）
	///     </seealso>
	///     (from MDN)
	/// </param>
	/// <param name="secure"></param>
	public void AddCookie(string id, string value,
		DateTime expires,
		CookieTagSameSite sameSite,
		string? domain = null,
		string? path = null,
		bool httpOnly = false,
		bool partitioned = false,
		bool secure = false)
	{
		StringBuilder cookie = new StringBuilder();
		cookie.Append($"{id}={value}");
		cookie.Append($"; Expires={expires.ToString("R")}");
		switch (sameSite)
		{
			case CookieTagSameSite.None:
				cookie.Append("; SameSite=None");
				break;
			case CookieTagSameSite.Lax:
				cookie.Append("; SameSite=Lax");
				break;
			case CookieTagSameSite.Strict:
				cookie.Append("; SameSite=Strict");
				break;
		}

		if (domain is not null) cookie.Append($"; Domain={domain}");
		if (path is not null) cookie.Append($"; Path={path}");
		if (httpOnly) cookie.Append("; HttpOnly");
		if (partitioned) cookie.Append("; Partitioned");
		if (secure) cookie.Append("; Secure");
		addHeader("Set-Cookie", cookie.ToString());
	}

	/// <summary>
	///     添加一个Set-Cookie头，告诉客户端添加一个Cookie
	/// </summary>
	/// <param name="id">Cookie id</param>
	/// <param name="value">Cookie 值</param>
	/// <param name="expires">Cookie 过期时间</param>
	/// <param name="domain">
	///     指定 cookie 可以送达的主机。
	///     只能将值设置为当前域名或更高级别的域名（除非是公共后缀）。设置域名将会使 cookie 对指定的域名及其所有子域名可用。
	///     若缺省，则此属性默认为当前文档 URL 的主机（不包括子域名）。
	///     多个主机/域名的值是不被允许的，但如果指定了一个域名，则其子域名也总会被包含。
	///     来自 <seealso href="https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Headers/Set-Cookie">MDN</seealso>
	///     归属于
	///     <seealso href="https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Headers/Set-Cookie/contributors.txt">Mozilla 贡献者</seealso>
	///     的署名和版权许可在 <seealso href="https://creativecommons.org/licenses/by-sa/2.5/deed.zh">知识共享 署名—相同方式共享 2.5</seealso> 许可下提供
	/// </param>
	/// <param name="path"></param>
	/// <param name="httpOnly">若为true，不能通过客户端的JavaScript脚本访问到该 Cookie</param>
	/// <param name="partitioned">
	///     表示应使用分区存储来存储 cookie。有关更多详细信息，请参见
	///     <seealso href="https://developer.mozilla.org/en-US/docs/Web/Privacy/Privacy_sandbox/Partitioned_cookies">
	///         具有独立分区状态的
	///         Cookie（CHIPS）
	///     </seealso>
	///     (from MDN)
	/// </param>
	/// <param name="secure"></param>
	public void addCookie(string id, string value,
		DateTime expires,
		string? domain = null,
		string? path = null,
		bool httpOnly = false,
		bool partitioned = false,
		bool secure = false)
	{
		StringBuilder cookie = new StringBuilder();
		cookie.Append($"{id}={value}");
		cookie.Append($"; Expires={expires.ToString("R")}");
		if (domain is not null) cookie.Append($"; Domain={domain}");
		if (path is not null) cookie.Append($"; Path={path}");
		if (httpOnly) cookie.Append("; HttpOnly");
		if (partitioned) cookie.Append("; Partitioned");
		if (secure) cookie.Append("; Secure");
		addHeader("Set-Cookie", cookie.ToString());
	}

	public void addCookie(string id, string value,
		int maxage,
		CookieTagSameSite sameSite,
		string? domain = null,
		string? path = null,
		bool httpOnly = false,
		bool partitioned = false,
		bool secure = false)
	{
		StringBuilder cookie = new StringBuilder();
		cookie.Append($"{id}={value}");
		cookie.Append($"; Max-Age={maxage}");
		switch (sameSite)
		{
			case CookieTagSameSite.None:
				cookie.Append("; SameSite=None");
				break;
			case CookieTagSameSite.Lax:
				cookie.Append("; SameSite=Lax");
				break;
			case CookieTagSameSite.Strict:
				cookie.Append("; SameSite=Strict");
				break;
		}

		if (domain is not null) cookie.Append($"; Domain={domain}");
		if (path is not null) cookie.Append($"; Path={path}");
		if (httpOnly) cookie.Append("; HttpOnly");
		if (partitioned) cookie.Append("; Partitioned");
		if (secure) cookie.Append("; Secure");
		addHeader("Set-Cookie", cookie.ToString());
	}

	public void addCookie(string id, string value,
		int maxage,
		string? domain = null,
		string? path = null,
		bool httpOnly = false,
		bool partitioned = false,
		bool secure = false)
	{
		StringBuilder cookie = new StringBuilder();
		cookie.Append($"{id}={value}");
		cookie.Append($"; Max-Age={maxage}");
		if (domain is not null) cookie.Append($"; Domain={domain}");
		if (path is not null) cookie.Append($"; Path={path}");
		if (httpOnly) cookie.Append("; HttpOnly");
		if (partitioned) cookie.Append("; Partitioned");
		if (secure) cookie.Append("; Secure");
		addHeader("Set-Cookie", cookie.ToString());
	}

	public void addCookie(string id, string value,
		CookieTagSameSite sameSite,
		string? domain = null,
		string? path = null,
		bool httpOnly = false,
		bool partitioned = false,
		bool secure = false)
	{
		StringBuilder cookie = new StringBuilder();
		cookie.Append($"{id}={value}");
		switch (sameSite)
		{
			case CookieTagSameSite.None:
				cookie.Append("; SameSite=None");
				break;
			case CookieTagSameSite.Lax:
				cookie.Append("; SameSite=Lax");
				break;
			case CookieTagSameSite.Strict:
				cookie.Append("; SameSite=Strict");
				break;
		}

		if (domain is not null) cookie.Append($"; Domain={domain}");
		if (path is not null) cookie.Append($"; Path={path}");
		if (httpOnly) cookie.Append("; HttpOnly");
		if (partitioned) cookie.Append("; Partitioned");
		if (secure) cookie.Append("; Secure");
		addHeader("Set-Cookie", cookie.ToString());
	}

	public void addCookie(string id, string value,
		string? domain = null,
		string? path = null,
		bool httpOnly = false,
		bool partitioned = false,
		bool secure = false)
	{
		StringBuilder cookie = new StringBuilder();
		cookie.Append($"{id}={value}");
		if (domain is not null) cookie.Append($"; Domain={domain}");
		if (path is not null) cookie.Append($"; Path={path}");
		if (httpOnly) cookie.Append("; HttpOnly");
		if (partitioned) cookie.Append("; Partitioned");
		if (secure) cookie.Append("; Secure");
		addHeader("Set-Cookie", cookie.ToString());
	}
}