using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomorrow_Is_Stock_King.Model
{
    public class Header
    {
        public string resultCode { get; set; }
        public string resultMsg { get; set; }
    }

    public class Item
    {
        public string srtnCd { get; set; }
        public string clpr { get; set; }
        public string itmsNm { get; set; }

    }

    public class Items
    {
        public IList<Item> item { get; set; }

    }

    public class Body
    {
        public int numOfRows { get; set; }
        public int pageNo { get; set; }
        public int totalCount { get; set; }
        public Items items { get; set; }
    }

    public class Response
    {
        public Header header { get; set; }
        public Body body { get; set; }
    }

    public class Example
    {
        public Response response { get; set; }
    }

}
