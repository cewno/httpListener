using System.Net;

namespace cewno.HttpListener;

/// <summary>
///     http请求头
/// </summary>
public class HttpRequestHeader : Dictionary<string, string>
{
	public Dictionary<string, string>? cookies
	{
		get => cookies;
		init => cookies = ParseCookies(this["Cookie"].ToCharArray());
	}

	/// <summary>
	///     解析cookie字符串为字典
	/// </summary>
	/// <param name="cookiesChar">包含cookie数据的字符数组</param>
	/// <param name="noThrow">当设置为true时，解析过程中遇到错误不会抛出异常</param>
	/// <param name="errEnd">当设置为true时，解析过程中遇到错误会终止解析</param>
	/// <returns>返回一个字典，其中包含解析后的cookie键值对。如果输入无任何有效内容，则返回null</returns>
	/// <exception cref="CookieException">遇到格式错误</exception>
	public static Dictionary<string, string>? ParseCookies(char[]? cookiesChar, bool noThrow = true, bool errEnd = true)
	{
		// 检查输入的cookie字符数组是否为空或长度不合法
		if (cookiesChar == null || cookiesChar.Length < 3) return null;

		Dictionary<string, string> cookies = new Dictionary<string, string>();
		int atStart = 0; // 记录当前处理的cookie子串的起始位置
		int atEqual = 0; // 记录等号 '=' 的位置
		bool noEqual = true; // 标记是否已经遇到了等号 '='

		// 遍历cookie字符数组，解析每个cookie键值对
		for (int i = 0; i < cookiesChar.Length; i++)
		{
			char at = cookiesChar[i];

			// 遇到等号 '='
			if (at == '=')
			{
				atEqual = i;

				// 如果已经遇到过等号，表示格式错误
				if (!noEqual)
				{
					atStart = i;
					if (!noThrow) throw new CookieException();
					if (errEnd) break;
				}

				noEqual = false;
			}
			// 遇到分号 ';'
			else if (at == ';')
			{
				// 如果没有遇到等号，表示格式错误
				if (noEqual)
				{
					if (!noThrow) throw new CookieException();
					if (errEnd) break;
					continue;
				}

				// 将当前解析到的cookie子串添加到字典中
				sub(cookiesChar, cookies, ref atStart, ref atEqual, i - 1);
				noEqual = true;
				atStart = i + 1;
			}
			// 处理字符串末尾
			else if (i == cookiesChar.Length - 1)
			{
				// 如果没有遇到等号，表示格式错误
				if (noEqual)
				{
					if (!noThrow) throw new CookieException();
					if (errEnd) break;
					continue;
				}

				// 处理最后一个cookie子串
				sub(cookiesChar, cookies, ref atStart, ref atEqual, ref i);
			}
		}

		return cookies;
	}

	// 这里假设存在一个名为sub的方法，用于处理具体的cookie子串解析逻辑


	private static void sub(
		char[] src,
		Dictionary<string, string> to,
		ref readonly int atStart,
		ref readonly int atEqual,
		ref readonly int atEnd)
	{
		to[new string(src, atStart, atEqual - atStart)]
			= new string(src, atEqual + 1, atEnd - atEqual);
	}
}