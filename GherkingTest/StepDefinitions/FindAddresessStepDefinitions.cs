using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;
using Forbrugsenhed_Sammenligner_gruppe_2;
using Forbrugsenhed_Sammenligner_gruppe_2.Components.Pages;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components.RenderTree;
using Bunit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentAssertions;

namespace GherkingTest.StepDefinitions
{
    [Binding]
    public class FindAddressesStepDefinitons
    {
        IRenderedComponent<Home> component = null;
        string addressOne = "";
        string addressTwo = "";
        [Given(@"im an anonymous user")]
        public void GivenImAnAnonymousUser() 
        { 
            //This is the default
        }
        [Given(@"I Have a fresh webpage open")]
        public void GivenNewPage()
        {
            using var ctx = new TestContext();
            var component = ctx.RenderComponent<Home>();
        }
        [Given(@"I Have the adress (.*)")]
        public void GivenIHaveTheAddress(string address)
        {
            addressOne = address;
        }
        [Given(@"I Have the second adress (.*)")]
        public void GivenIHaveTheSecondAddress(string address)
        {
            addressTwo = address;
        }
        [When(@"I input the 1st address into the  (.*)")]
        public void IInputThe1Address(string line)
        {
            var paraElm = component.Find(line);
            paraElm.TextContent = addressOne;
        }
        [When(@"I input the 2nd address into the  (.*)")]
        public void IInputThe2Address(string line)
        {
            var paraElm = component.Find(line);
            paraElm.TextContent = addressTwo;
            
        }
        [When(@"I click (.*)")]
        public void WhenIclick(string line)
        {
            component.Find(line).Click();
        }
        [Then(@"the result should be displayed for the (.*) address")]
        public void TheResltShouldBeDisplayed()
        {
            component.Find("display").Should().NotBeNull();
        }
        [Then(@"an error should be displayed showing where i made a mistake")]
        public void AnErrorShouldBeDisplayed()
        {
            component.Find("Error").Should().NotBeNull();
        }
        [Then(@"a popup should alert me that i got more than one result")]
        public void APopupShouldAlertMeThatIGotMoreThanOneResult()
        {
            component.Find("Popup").Should().NotBeNull();

        }

    }
}
