using System.Text;

namespace cewno.HttpListener;

public sealed class HttpStatusCodes
{
	public static readonly byte[][] id = new byte[600][];
	public static readonly int HTTPCODE_100_CONTINUE = 100;
	public static readonly int HTTPCODE_101_SWITCHING_PROTOCOLS = 101;
	public static readonly int HTTPCODE_102_PROCESSING = 102;
	public static readonly int HTTPCODE_103_EARLY_HINTS = 103;
	public static readonly int HTTPCODE_200_OK = 200;
	public static readonly int HTTPCODE_201_CREATED = 201;
	public static readonly int HTTPCODE_202_ACCEPTED = 202;
	public static readonly int HTTPCODE_203_NON_AUTHORITATIVE_INFORMATION = 203;
	public static readonly int HTTPCODE_204_NO_CONTENT = 204;
	public static readonly int HTTPCODE_205_RESET_CONTENT = 205;
	public static readonly int HTTPCODE_206_PARTIAL_CONTENT = 206;
	public static readonly int HTTPCODE_207_MULTI_STATUS = 207;
	public static readonly int HTTPCODE_208_ALREADY_REPORTED = 208;
	public static readonly int HTTPCODE_226_IM_USED = 226;
	public static readonly int HTTPCODE_300_MULTIPLE_CHOICE = 300;
	public static readonly int HTTPCODE_301_MOVED_PERMANENTLY = 301;
	public static readonly int HTTPCODE_302_FOUND = 302;
	public static readonly int HTTPCODE_303_SEE_OTHER = 303;
	public static readonly int HTTPCODE_304_NOT_MODIFIED = 304;
	public static readonly int HTTPCODE_305_USE_PROXY = 305;
	public static readonly int HTTPCODE_306_UNUSED = 306;
	public static readonly int HTTPCODE_307_TEMPORARY_REDIRECT = 307;
	public static readonly int HTTPCODE_308_PERMANENT_REDIRECT = 308;
	public static readonly int HTTPCODE_400_BAD_REQUEST = 400;
	public static readonly int HTTPCODE_401_UNAUTHORIZED = 401;
	public static readonly int HTTPCODE_402_PAYMENT_REQUIRED = 402;
	public static readonly int HTTPCODE_403_FORBIDDEN = 403;
	public static readonly int HTTPCODE_404_NOT_FOUND = 404;
	public static readonly int HTTPCODE_405_METHOD_NOT_ALLOWED = 405;
	public static readonly int HTTPCODE_406_NOT_ACCEPTABLE = 406;
	public static readonly int HTTPCODE_407_PROXY_AUTHENTICATION_REQUIRED = 407;
	public static readonly int HTTPCODE_408_REQUEST_TIMEOUT = 408;
	public static readonly int HTTPCODE_409_CONFLICT = 409;
	public static readonly int HTTPCODE_410_GONE = 410;
	public static readonly int HTTPCODE_411_LENGTH_REQUIRED = 411;
	public static readonly int HTTPCODE_412_PRECONDITION_FAILED = 412;
	public static readonly int HTTPCODE_413_PAYLOAD_TOO_LARGE = 413;
	public static readonly int HTTPCODE_414_URI_TOO_LONG = 414;
	public static readonly int HTTPCODE_415_UNSUPPORTED_MEDIA_TYPE = 415;
	public static readonly int HTTPCODE_416_RANGE_NOT_SATISFIABLE = 416;
	public static readonly int HTTPCODE_417_EXPECTATION_FAILED = 417;
	public static readonly int HTTPCODE_418_IM_A_TEAPOT = 418;
	public static readonly int HTTPCODE_421_MISDIRECTED_REQUEST = 421;
	public static readonly int HTTPCODE_422_UNPROCESSABLE_ENTITY = 422;
	public static readonly int HTTPCODE_423_LOCKED = 423;
	public static readonly int HTTPCODE_424_FAILED_DEPENDENCY = 424;
	public static readonly int HTTPCODE_425_TOO_EARLY = 425;
	public static readonly int HTTPCODE_426_UPGRADE_REQUIRED = 426;
	public static readonly int HTTPCODE_428_PRECONDITION_REQUIRED = 428;
	public static readonly int HTTPCODE_429_TOO_MANY_REQUESTS = 429;
	public static readonly int HTTPCODE_431_REQUEST_HEADER_FIELDS_TOO_LARGE = 431;
	public static readonly int HTTPCODE_451_UNAVAILABLE_FOR_LEGAL_REASONS = 451;
	public static readonly int HTTPCODE_500_INTERNAL_SERVER_ERROR = 500;
	public static readonly int HTTPCODE_501_NOT_IMPLEMENTED = 501;
	public static readonly int HTTPCODE_502_BAD_GATEWAY = 502;
	public static readonly int HTTPCODE_503_SERVICE_UNAVAILABLE = 503;
	public static readonly int HTTPCODE_504_GATEWAY_TIMEOUT = 504;
	public static readonly int HTTPCODE_505_HTTP_VERSION_NOT_SUPPORTED = 505;
	public static readonly int HTTPCODE_506_VARIANT_ALSO_NEGOTIATES = 506;
	public static readonly int HTTPCODE_507_INSUFFICIENT_STORAGE = 507;
	public static readonly int HTTPCODE_508_LOOP_DETECTED = 508;
	public static readonly int HTTPCODE_510_NOT_EXTENDED = 510;
	public static readonly int HTTPCODE_511_NETWORK_AUTHENTICATION_REQUIRED = 511;

	static HttpStatusCodes()
	{
		id[100] = [49, 48, 48, 32, 99, 111, 110, 116, 105, 110, 117, 101];
		id[101] =
		[
			49, 48, 49, 32, 115, 119, 105, 116, 99, 104, 105, 110, 103, 95, 112, 114, 111, 116, 111, 99, 111, 108, 115
		];
		id[102] = [49, 48, 50, 32, 112, 114, 111, 99, 101, 115, 115, 105, 110, 103];
		id[103] = [49, 48, 51, 32, 101, 97, 114, 108, 121, 95, 104, 105, 110, 116, 115];
		id[200] = [50, 48, 48, 32, 111, 107];
		id[201] = [50, 48, 49, 32, 99, 114, 101, 97, 116, 101, 100];
		id[202] = [50, 48, 50, 32, 97, 99, 99, 101, 112, 116, 101, 100];
		id[203] =
		[
			50, 48, 51, 32, 110, 111, 110, 45, 97, 117, 116, 104, 111, 114, 105, 116, 97, 116, 105, 118, 101, 95, 105,
			110, 102, 111, 114, 109, 97, 116, 105, 111, 110
		];
		id[204] = [50, 48, 52, 32, 110, 111, 95, 99, 111, 110, 116, 101, 110, 116];
		id[205] = [50, 48, 53, 32, 114, 101, 115, 101, 116, 95, 99, 111, 110, 116, 101, 110, 116];
		id[206] = [50, 48, 54, 32, 112, 97, 114, 116, 105, 97, 108, 95, 99, 111, 110, 116, 101, 110, 116];
		id[207] = [50, 48, 55, 32, 109, 117, 108, 116, 105, 45, 115, 116, 97, 116, 117, 115];
		id[208] = [50, 48, 56, 32, 97, 108, 114, 101, 97, 100, 121, 95, 114, 101, 112, 111, 114, 116, 101, 100];
		id[226] = [50, 50, 54, 32, 105, 109, 95, 117, 115, 101, 100];
		id[300] = [51, 48, 48, 32, 109, 117, 108, 116, 105, 112, 108, 101, 95, 99, 104, 111, 105, 99, 101];
		id[301] = [51, 48, 49, 32, 109, 111, 118, 101, 100, 95, 112, 101, 114, 109, 97, 110, 101, 110, 116, 108, 121];
		id[302] = [51, 48, 50, 32, 102, 111, 117, 110, 100];
		id[303] = [51, 48, 51, 32, 115, 101, 101, 95, 111, 116, 104, 101, 114];
		id[304] = [51, 48, 52, 32, 110, 111, 116, 95, 109, 111, 100, 105, 102, 105, 101, 100];
		id[305] = [51, 48, 53, 32, 117, 115, 101, 95, 112, 114, 111, 120, 121];
		id[306] = [51, 48, 54, 32, 117, 110, 117, 115, 101, 100];
		id[307] =
		[
			51, 48, 55, 32, 116, 101, 109, 112, 111, 114, 97, 114, 121, 95, 114, 101, 100, 105, 114, 101, 99, 116
		];
		id[308] =
		[
			51, 48, 56, 32, 112, 101, 114, 109, 97, 110, 101, 110, 116, 95, 114, 101, 100, 105, 114, 101, 99, 116
		];
		id[400] = [52, 48, 48, 32, 98, 97, 100, 95, 114, 101, 113, 117, 101, 115, 116];
		id[401] = [52, 48, 49, 32, 117, 110, 97, 117, 116, 104, 111, 114, 105, 122, 101, 100];
		id[402] = [52, 48, 50, 32, 112, 97, 121, 109, 101, 110, 116, 95, 114, 101, 113, 117, 105, 114, 101, 100];
		id[403] = [52, 48, 51, 32, 102, 111, 114, 98, 105, 100, 100, 101, 110];
		id[404] = [52, 48, 52, 32, 110, 111, 116, 95, 102, 111, 117, 110, 100];
		id[405] =
		[
			52, 48, 53, 32, 109, 101, 116, 104, 111, 100, 95, 110, 111, 116, 95, 97, 108, 108, 111, 119, 101, 100
		];
		id[406] = [52, 48, 54, 32, 110, 111, 116, 95, 97, 99, 99, 101, 112, 116, 97, 98, 108, 101];
		id[407] =
		[
			52, 48, 55, 32, 112, 114, 111, 120, 121, 95, 97, 117, 116, 104, 101, 110, 116, 105, 99, 97, 116, 105, 111,
			110, 95, 114, 101, 113, 117, 105, 114, 101, 100
		];
		id[408] = [52, 48, 56, 32, 114, 101, 113, 117, 101, 115, 116, 95, 116, 105, 109, 101, 111, 117, 116];
		id[409] = [52, 48, 57, 32, 99, 111, 110, 102, 108, 105, 99, 116];
		id[410] = [52, 49, 48, 32, 103, 111, 110, 101];
		id[411] = [52, 49, 49, 32, 108, 101, 110, 103, 116, 104, 95, 114, 101, 113, 117, 105, 114, 101, 100];
		id[412] =
		[
			52, 49, 50, 32, 112, 114, 101, 99, 111, 110, 100, 105, 116, 105, 111, 110, 95, 102, 97, 105, 108, 101, 100
		];
		id[413] = [52, 49, 51, 32, 112, 97, 121, 108, 111, 97, 100, 95, 116, 111, 111, 95, 108, 97, 114, 103, 101];
		id[414] = [52, 49, 52, 32, 117, 114, 105, 95, 116, 111, 111, 95, 108, 111, 110, 103];
		id[415] =
		[
			52, 49, 53, 32, 117, 110, 115, 117, 112, 112, 111, 114, 116, 101, 100, 95, 109, 101, 100, 105, 97, 95, 116,
			121, 112, 101
		];
		id[416] =
		[
			52, 49, 54, 32, 114, 97, 110, 103, 101, 95, 110, 111, 116, 95, 115, 97, 116, 105, 115, 102, 105, 97, 98,
			108, 101
		];
		id[417] =
		[
			52, 49, 55, 32, 101, 120, 112, 101, 99, 116, 97, 116, 105, 111, 110, 95, 102, 97, 105, 108, 101, 100
		];
		id[418] = [52, 49, 56, 32, 105, 109, 95, 97, 95, 116, 101, 97, 112, 111, 116];
		id[421] =
		[
			52, 50, 49, 32, 109, 105, 115, 100, 105, 114, 101, 99, 116, 101, 100, 95, 114, 101, 113, 117, 101, 115, 116
		];
		id[422] =
		[
			52, 50, 50, 32, 117, 110, 112, 114, 111, 99, 101, 115, 115, 97, 98, 108, 101, 95, 101, 110, 116, 105, 116,
			121
		];
		id[423] = [52, 50, 51, 32, 108, 111, 99, 107, 101, 100];
		id[424] = [52, 50, 52, 32, 102, 97, 105, 108, 101, 100, 95, 100, 101, 112, 101, 110, 100, 101, 110, 99, 121];
		id[425] = [52, 50, 53, 32, 116, 111, 111, 95, 101, 97, 114, 108, 121];
		id[426] = [52, 50, 54, 32, 117, 112, 103, 114, 97, 100, 101, 95, 114, 101, 113, 117, 105, 114, 101, 100];
		id[428] =
		[
			52, 50, 56, 32, 112, 114, 101, 99, 111, 110, 100, 105, 116, 105, 111, 110, 95, 114, 101, 113, 117, 105, 114,
			101, 100
		];
		id[429] = [52, 50, 57, 32, 116, 111, 111, 95, 109, 97, 110, 121, 95, 114, 101, 113, 117, 101, 115, 116, 115];
		id[431] =
		[
			52, 51, 49, 32, 114, 101, 113, 117, 101, 115, 116, 95, 104, 101, 97, 100, 101, 114, 95, 102, 105, 101, 108,
			100, 115, 95, 116, 111, 111, 95, 108, 97, 114, 103, 101
		];
		id[451] =
		[
			52, 53, 49, 32, 117, 110, 97, 118, 97, 105, 108, 97, 98, 108, 101, 95, 102, 111, 114, 95, 108, 101, 103, 97,
			108, 95, 114, 101, 97, 115, 111, 110, 115
		];
		id[500] =
		[
			53, 48, 48, 32, 105, 110, 116, 101, 114, 110, 97, 108, 95, 115, 101, 114, 118, 101, 114, 95, 101, 114, 114,
			111, 114
		];
		id[501] = [53, 48, 49, 32, 110, 111, 116, 95, 105, 109, 112, 108, 101, 109, 101, 110, 116, 101, 100];
		id[502] = [53, 48, 50, 32, 98, 97, 100, 95, 103, 97, 116, 101, 119, 97, 121];
		id[503] =
		[
			53, 48, 51, 32, 115, 101, 114, 118, 105, 99, 101, 95, 117, 110, 97, 118, 97, 105, 108, 97, 98, 108, 101
		];
		id[504] = [53, 48, 52, 32, 103, 97, 116, 101, 119, 97, 121, 95, 116, 105, 109, 101, 111, 117, 116];
		id[505] =
		[
			53, 48, 53, 32, 104, 116, 116, 112, 95, 118, 101, 114, 115, 105, 111, 110, 95, 110, 111, 116, 95, 115, 117,
			112, 112, 111, 114, 116, 101, 100
		];
		id[506] =
		[
			53, 48, 54, 32, 118, 97, 114, 105, 97, 110, 116, 95, 97, 108, 115, 111, 95, 110, 101, 103, 111, 116, 105,
			97, 116, 101, 115
		];
		id[507] =
		[
			53, 48, 55, 32, 105, 110, 115, 117, 102, 102, 105, 99, 105, 101, 110, 116, 95, 115, 116, 111, 114, 97, 103,
			101
		];
		id[508] = [53, 48, 56, 32, 108, 111, 111, 112, 95, 100, 101, 116, 101, 99, 116, 101, 100];
		id[510] = [53, 49, 48, 32, 110, 111, 116, 95, 101, 120, 116, 101, 110, 100, 101, 100];
		id[511] =
		[
			53, 49, 49, 32, 110, 101, 116, 119, 111, 114, 107, 95, 97, 117, 116, 104, 101, 110, 116, 105, 99, 97, 116,
			105, 111, 110, 95, 114, 101, 113, 117, 105, 114, 101, 100
		];
	}

	public static byte[] GetCodeMsg(int code)
	{
		byte[] msg = id[code];
		if (msg == null) return Encoding.UTF8.GetBytes(code.ToString());

		return msg;
	}
}