using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // [ViewContext] attribute
using Microsoft.AspNetCore.Mvc.Rendering;       // ViewContext data type
using Microsoft.AspNetCore.Routing;             // LinkGenerator
using GameStore.Models.Grid;
using GameStore.Models.ExtensionModels;

namespace GameStore.TagHelpers
{
    [HtmlTargetElement("select", Attributes = "my-min-number, my-max-number")]
    public class NumberDropDownTagHelper : TagHelper
    {
        [HtmlAttributeName("my-min-number")]
        public int Min { get; set; }
        [HtmlAttributeName("my-max-number")]
        public int Max { get; set; }

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            // get selected value from view's model
            ModelExpression aspfor =
                (ModelExpression)context.AllAttributes["asp-for"].Value;
            int modelValue = (int)aspfor?.Model;

            for (int i = Min; i <= Max; i++)
            {
                TagBuilder option = new TagBuilder("option");
                option.InnerHtml.Append(i.ToString());

                // mark option as selected if matches model’s value
                if (modelValue == i)
                    option.Attributes["selected"] = "selected";

                output.Content.AppendHtml(option);
            }
        }
    }
}
