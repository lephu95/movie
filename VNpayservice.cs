using System.Security.Cryptography;
using System.Text;

namespace movie.Services.Implement
{
    public class VNpayservice
    {
        
        // VNPayService.cs
        public class VNPayService
        {
            private readonly string _vnp_TmnCode = "YOUR_VNPAY_TMNCODE"; // Mã cửa hàng VNPay của bạn
            private readonly string _vnp_HashSecret = "YOUR_VNPAY_HASH_SECRET"; // Khóa bí mật dùng để tạo mã hash
            private readonly IHttpContextAccessor _httpContextAccessor;

            public VNPayService(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public string GeneratePaymentUrl(decimal amount, string orderId)
            {

                var vnp_Params = new SortedDictionary<string, string>();
                vnp_Params.Add("vnp_Version", "2.0.0");
                vnp_Params.Add("vnp_Command", "pay");
                vnp_Params.Add("vnp_TmnCode", _vnp_TmnCode);
                vnp_Params.Add("vnp_Locale", "vn");
                vnp_Params.Add("vnp_CurrCode", "VND");
                vnp_Params.Add("vnp_TxnRef", orderId);
                vnp_Params.Add("vnp_OrderInfo", "Payment for order " + orderId);
                vnp_Params.Add("vnp_OrderType", "billpayment");
                vnp_Params.Add("vnp_Amount", (amount * 100).ToString()); // Convert amount to cents
                vnp_Params.Add("vnp_ReturnUrl", "YOUR_RETURN_URL"); // Đường dẫn trả về sau khi thanh toán thành công
                vnp_Params.Add("vnp_IpAddr", GetIpAddress());
                vnp_Params.Add("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

                string vnp_Url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
                string vnp_HashSecret = _vnp_HashSecret;

                string vnp_SecureHash = VNPayUtils.VnpayHash(vnp_Params, vnp_HashSecret);

                vnp_Params.Add("vnp_SecureHashType", "MD5");
                vnp_Params.Add("vnp_SecureHash", vnp_SecureHash);

                string queryString = VNPayUtils.CreateQueryString(vnp_Params);

                return vnp_Url + "?" + queryString;
            }

            private string GetIpAddress()
            {
                var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                return ipAddress;
            }
        }

        // VNPayUtils.cs
        public static class VNPayUtils
        {
            public static string VnpayHash(SortedDictionary<string, string> fields, string secretKey)
            {
                StringBuilder hashData = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in fields)
                {
                    if (!string.IsNullOrEmpty(kv.Value))
                    {
                        hashData.Append(kv.Key + "=" + kv.Value + "&");
                    }
                }
                hashData.Append("vnp_HashSecret=" + secretKey);
                string hash = Sha256(hashData.ToString());
                return hash;
            }

            public static string Sha256(string rawData)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }

            public static string CreateQueryString(SortedDictionary<string, string> fields)
            {
                StringBuilder queryString = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in fields)
                {
                    queryString.Append(Uri.EscapeDataString(kv.Key) + "=" + Uri.EscapeDataString(kv.Value) + "&");
                }
                return queryString.ToString().TrimEnd('&');
            }
        }

    }
}
