using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CustomTagHelper.TagHelpers
{
    [HtmlTargetElement("pre")]
    [HtmlTargetElement("hljs")]
    public class HighLightJSTaghelper : TagHelper
    {
        
        [HtmlAttributeName("hljs-lang")]
        public string Language { get; set; }
        [HtmlAttributeName("style")]
        public string Style { get; set; }
        [HtmlAttributeName("script")]
        public string Script { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           
            TagBuilder code = new TagBuilder("code");
            code.AddCssClass($"{Language}");
            
            var content = (output.GetChildContentAsync().Result).GetContent();

            code.InnerHtml.AppendHtml(content);
            
            StringWriter ms = new StringWriter();
            code.WriteTo(ms,HtmlEncoder.Default);

            output.Content.SetHtmlContent(ms.ToString());
        }
        //private string Escape(string input)
        //{
        //    return OutputElementHintAttribute;
        //}
        

    }
    
}