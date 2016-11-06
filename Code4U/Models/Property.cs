using DatabaseSchemaReader.DataSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Models
{
    public class Property
    {
        public DatabaseColumn DbColumn { get; set; }

        public Property(DatabaseColumn dbColummn)
        {
            this.DbColumn = dbColummn;
        }

        public string Name
        {
            get
            {
                if (this.DbColumn.IsForeignKey) return this.DbColumn.ForeignKeyTableName;

                return this.DbColumn.Name;
            }
        }

        public string Type
        {
            get
            {
                if (this.DbColumn.IsForeignKey) return this.DbColumn.ForeignKeyTableName;

                return this.DbColumn.DataType.NetDataTypeCSharpName;
            }
        }

        public int? Size { get { return this.DbColumn.Length; } }

        public bool Identity
        {
            get
            {
                return this.DbColumn.IsAutoNumber;
            }
        }

        public bool PrimaryKey
        {
            get
            {
                return this.DbColumn.IsPrimaryKey;
            }
        }

        public bool IsComplexType
        {
            get
            {
                return this.DbColumn.IsForeignKey;
            }
        }
    }
}