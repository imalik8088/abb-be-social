using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Data;

namespace ServerService
{
    [DataContract]
    [KnownType(typeof(SqlParameter[]))]
    [KnownType(typeof(object))]
    [KnownType(typeof(SqlDbType))]
    [KnownType(typeof(ParameterDirection))]
    //[KnownType(typeof(SqlDateTime))]
    public class SQLParameter
    {
        private string paramaterName;
        private object paramaterValue;
        private string size;
        private ParameterDirection paramaterDirection;
        private SqlDbType parameterType;

        [DataMember]
        public SqlDbType ParameterType
        {
            get { return parameterType; }
            set { parameterType = value; }
        }

        [DataMember]
        public string ParamaterName
        {
            get { return paramaterName; }
            set { paramaterName = value; }
        }

        [DataMember(IsRequired = false)]
        public object ParamaterValue
        {
            get { return paramaterValue; }
            set { paramaterValue = value; }
        }

        [DataMember]
        public ParameterDirection ParamaterDirection
        {
            get { return paramaterDirection; }
            set { paramaterDirection = value; }
        }

        [DataMember]
        public string Size
        {
            get { return size; }
            set { size = value; }
        }
    }
}