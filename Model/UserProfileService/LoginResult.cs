using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    /// <summary>
    /// A Custom VO which keeps the results for a login action.
    /// </summary>
    [Serializable()]
    public class LoginResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResult"/> class.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="firstName">Users's first name.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        public LoginResult(long userId, string firstName, string encryptedPassword, string country,   string language )
        {
            this.country = country;
            this.encryptedPassword = encryptedPassword;
            this.firstName = firstName;
            this.language = language;
            this.userId = userId;
        }


        #region Properties Region

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string country { get; private set; }

        /// <summary>
        /// Gets the encrypted password.
        /// </summary>
        /// <value>The <c>encryptedPassword.</c></value>
        public string encryptedPassword { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>The <c>firstName</c></value>
        public string firstName { get; private set; }

        /// <summary>
        /// Gets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string language { get; private set; }

        /// <summary>
        /// Gets the user profile id.
        /// </summary>
        /// <value>The user profile id.</value>
        public long userId { get; private set; }

        #endregion Properties Region

        public override bool Equals(object obj)
        {
            LoginResult target = (LoginResult)obj;

            return (this.userId == target.userId)
                   && (this.firstName == target.firstName)
                   && (this.encryptedPassword == target.encryptedPassword)
                   && (this.language == target.language)
                   && (this.country == target.country);
        }

        // The GetHashCode method is used in hashing algorithms and data
        // structures such as a hash table. In order to ensure that it works
        // properly, it is based on a field that does not change.
        public override int GetHashCode()
        {
            return this.userId.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strLoginResult;

            strLoginResult =
                "[ userProfileId = " + userId + " | " +
                "firstName = " + firstName + " | " +
                "encryptedPassword = " + encryptedPassword + " | " +
                "language = " + language + " | " +
                "country = " + country + " ]";

            return strLoginResult;
        }
    }
}