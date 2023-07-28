using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.BusinessLogic.Constants
{
    public class ErrorMessages
    {
        public const string MandatoryAddressMissing = "Customer should have atleast one address";
        public const string CustomerIsNotUnique = "A customer already exists for the provided email address";
        public const string CustomerDoesNotExist = "A customer does not exist for the given id";
        public const string PrimaryAddressExists = "A main address already exists for the Customer";
        public const string AtleastOneAddressMandatory = "A customer must have atleast one address";
        public const string PrimaryAddressCannotBeRemoved = "Primary address cannot be deleted";
        public const string PrimaryAddressMandatory = "One address must be primary";
        public const string MandatoryAddressLineOne = "Please specify address line one";
        public const string MaxLengthAddressLineOne = "MaximumLength must be 80 characters for address line one";
        public const string MaxLengthAddressLineTwo = "MaximumLength must be 80 characters for address line two";
        public const string MandatoryPostCode = "Please specify a postcode";
        public const string MaxLengthPostCode = "MaximumLength must be 10 characters for postcode";
        public const string MandatoryTown = "Please specify a town";
        public const string MaxLengthTown = "MaximumLength must be 50 characters for town";
        public const string MaxLengthCounty = "MaximumLength must be 50 characters for county";
        public const string MandatoryTitle = "Please specify title";
        public const string MaxLengthTitle = "MaximumLength must be 20 characters for title";
        public const string MaxLengthSurname = "MaximumLength must be 50 characters for surname";
        public const string MandatorySurname = "Please specify a surname";
        public const string MaxLengthForename = "MaximumLength must be 50 characters for forename";
        public const string MandatoryForename = "Please specify a forename";
        public const string MaxLengthEmailAddress = "MaximumLength must be 75 characters for email address";
        public const string InvalidEmailAddress = "Invalid email address";
        public const string MaxLengthMobileNumber = "MaximumLength must be 15 characters for mobile address";
        public const string MandatoryMobileNumber = "Please specify a mobile number";
    }
}
