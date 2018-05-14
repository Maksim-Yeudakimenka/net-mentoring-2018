using System.Xml.Xsl;

namespace XslTransformation
{
  class Program
  {
    static void Main(string[] args)
    {
      var xsl = new XslCompiledTransform();
      xsl.Load("RssTransformation.xslt");

      xsl.Transform("books.xml", "result.rss");
    }
  }
}