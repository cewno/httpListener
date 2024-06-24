using System.Collections;
using System.Text.RegularExpressions;

namespace cewno.HttpListener;

public class HttpPathRegexRoute : IHttpRoute
{
	protected Hashtable _hashtable = new Hashtable();

	public void Route(HttpContext context)
	{
		string path = context.uri.AbsolutePath;
		foreach (Regex result in _hashtable.Keys.Cast<Regex>())
			if (result.IsMatch(path))
			{
				((Action<HttpContext>)_hashtable[result]).Invoke(context);
				return;
			}
	}

	public void Reg(Regex regex, Action<HttpContext> action)
	{
		_hashtable[regex] = action;
	}
}