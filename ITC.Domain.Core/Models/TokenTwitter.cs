using System;

namespace ITC.Domain.Core.Models
{
    public class TokenTwitter : Entity
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        /// <summary>
        /// Sô lần đã đăng
        /// </summary>
        public int AmountPosted { get; set; }
        /// <summary>
        /// Có xóa hay không
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
