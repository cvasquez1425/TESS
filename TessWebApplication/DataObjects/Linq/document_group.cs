using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Greenspoon.Tess.Classes;

namespace Greenspoon.Tess.DataObjects.Linq
{
    public partial class document_group
    {
        internal static List<DropDownItem> GetDocumentGroupList() {
            var documentGroupList = new List<DropDownItem>();
            using(var ctx = DataContextFactory.CreateContext()) {
                var docGroups = (from d in ctx.document_group
                                orderby d.document_group_name ascending
                                select new
                                {
                                    Name = d.document_group_name,
                                    Value = d.document_group_id
                                }).ToList();
                foreach(var item in docGroups) {
                    documentGroupList.Add(new DropDownItem { Name = item.Name, Value = item.Value.ToString() });
                }
                documentGroupList.Insert(0, new DropDownItem());
            }
            return documentGroupList;
        }
    }// end of class
}