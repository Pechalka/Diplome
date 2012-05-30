using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Tests
{
    /// <summary>
    /// Represents a base member finder convention where all read/write fields and properties are serialized based on binding flags.
    /// </summary>
    public abstract class BindingFlagsMemberFinderConvention : IMemberFinderConvention
    {
        // private fields
        private const BindingFlags ValidMemberBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly BindingFlags memberBindingFlags;

        // constructors
        /// <summary>
        /// Initializes an instance of the BindingFlagsMemberFinderConvention class.
        /// </summary>
        /// <param name="memberBindingFlags">The member binding flags.</param>
        protected BindingFlagsMemberFinderConvention(BindingFlags memberBindingFlags)
        {
            if ((memberBindingFlags & ~ValidMemberBindingFlags) != 0)
            {
                throw new ArgumentException("Invalid binding flags '" + memberBindingFlags + "'", "memberBindingFlags");
            }

            this.memberBindingFlags = memberBindingFlags;
        }

        /// <summary>
        /// Finds the members of a class that are serialized.
        /// </summary>
        /// <param name="type">The class.</param>
        /// <returns>The members that are serialized.</returns>
        public IEnumerable<MemberInfo> FindMembers(Type type)
        {
            foreach (var fieldInfo in type.GetFields(memberBindingFlags | BindingFlags.DeclaredOnly))
            {
                if (fieldInfo.IsInitOnly || fieldInfo.IsLiteral)
                {
                    // we can't write
                    continue;
                }

                if (fieldInfo.IsPrivate && fieldInfo.IsDefined(typeof(CompilerGeneratedAttribute), false))
                {
                    // skip private compiler generated backing fields
                    continue;
                }

                yield return fieldInfo;
            }

            foreach (var propertyInfo in type.GetProperties(memberBindingFlags | BindingFlags.DeclaredOnly))
            {
                if (!propertyInfo.CanRead || (!propertyInfo.CanWrite && type.Namespace != null))
                {
                    // we can't read, or we can't write and it is not anonymous
                    continue;
                }

                // skip indexers
                if (propertyInfo.GetIndexParameters().Length != 0)
                {
                    continue;
                }

                // skip overridden properties (they are already included by the base class)
                var getMethodInfo = propertyInfo.GetGetMethod(true);
                if (getMethodInfo.IsVirtual && getMethodInfo.GetBaseDefinition().DeclaringType != type)
                {
                    continue;
                }

                yield return propertyInfo;
            }
        }
    }
    public class PrivateMemberFinderConvention : BindingFlagsMemberFinderConvention
    {
        public PrivateMemberFinderConvention()
            : base(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
        }
    }

    public class MongoHelper
    {
        public static MongoCollection<TDocument> GetCollection<TDocument>()
        {
            string collectionName = typeof (TDocument).Name;
            return GetCollection<TDocument>(collectionName, "Diplome");
        }

        public static MongoCollection<TDocument> GetCollection<TDocument>(string collectionName, string dataBaseName)
        {
            var server = MongoServer.Create();
            var database = server.GetDatabase(dataBaseName);
            return database.GetCollection<TDocument>(collectionName);
        }

        //public static void SetPrivateFieldsFindOn()
        //{
        //    var conventionProfile = ConventionProfile.GetDefault()
        //        .SetMemberFinderConvention(new PrivateMemberFinderConvention());

        //    BsonClassMap.RegisterConventions(conventionProfile, type => type.BaseType == typeof(Entity));

        //}
    }
}