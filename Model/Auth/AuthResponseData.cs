namespace GeoGeometry.Model.Auth {
    public class AuthResponseData : BaseResponseObject
    {
        /// <summary>
        /// Код клиента.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Email клиента.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя клиента.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия клиента.
        /// </summary>
        public string LastName { get; set; }
        
        public string Role { get; set; }
    }
}
     

        