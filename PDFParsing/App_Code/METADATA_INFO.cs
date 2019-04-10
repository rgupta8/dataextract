using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PDFParsing
{
    public class METADATA_INFO
    {
        //PDFParseDataContext dbcontext = new PDFParseDataContext();
        public void GET_DOCUMENT_METAINFO(string Path, int DocumentId)
        {
            //PdfReader reader
            //    = new PdfReader(Path);
            //Dictionary<string, string> infodict = reader.Info;
            //DOCUMENT_METATAG metaTag = new DOCUMENT_METATAG();
            //foreach (KeyValuePair<string, string> kvp in infodict)
            //{
            //    metaTag.DOCUMENT_ID = DocumentId;
            //    metaTag.TITLE = kvp.Key;
            //    metaTag.VALUE = kvp.Value;
            //    dbcontext.DOCUMENT_METATAGs.InsertOnSubmit(metaTag);
            //    dbcontext.SubmitChanges();
            //}
        }
    }
}