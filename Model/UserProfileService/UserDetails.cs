using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileService
{
    public class UserDetails
    {
        public String firstName { get; private set; }

        public String lastname { get; private set; }

        public String email { get; private set; }

        public String language { get; private set; }

        public String country { get; private set; }

        public int NumberPhotos { get; set; }

        public int NumberFollowed { get; set; }

        public int NumberCreators { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDetails"/>
        /// class.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="email">The user's email.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        public UserDetails(String firstName, String lastName,
            String email, String language, String country)
        {
            this.firstName = firstName;
            this.lastname = lastName;
            this.email = email;
            this.language = language;
            this.country = country;
        }

        public override bool Equals(object obj)
        {

            UserDetails target = (UserDetails)obj;

            return (this.firstName == target.firstName)
                  && (this.lastname == target.lastname)
                  && (this.email == target.email)
                  && (this.language == target.language)
                  && (this.country == target.country);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.firstName.GetHashCode();
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
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ firstName = " + firstName + " | " +
                "lastName = " + lastname + " | " +
                "email = " + email + " | " +
                "language = " + language + " | " +
                "country = " + country + " ]";


            return strUserProfileDetails;
        }
    }
}

