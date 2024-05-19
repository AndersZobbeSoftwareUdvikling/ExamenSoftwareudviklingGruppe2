using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;
using Bunit;
using Forbrugsenhed_Sammenligner_gruppe_2.Components.Pages;
using FluentAssertions;


namespace GherkingTest.StepDefinitions
{
    [Binding]
    public class FindAddressStepDefinitons
    {
        IRenderedComponent<Home> component = null;
        string addressOne = "";
        string addressTwo = "";
        [Given(@"im an anonymous user")]
        public void Givenimananonymususer() 
        { 
            // this is the default
        }
        [Given(@"I Have a fresh webpage open")]
        public void GivenNewPage()
        {
            using var ctx = new TestContext();
            var component = ctx.RenderComponent<Home>();
        }
        [Given(@"I Have the adress (.*)")]
        public void Givenihavetheaddress(string address)
        {
            addressOne = address;
        }
        [When(@"I input the address into the (.*)")]
        public void IInputTheAddressIntoThe(string line)
        {

            var paraElm = component.Find(line);
            paraElm.TextContent = addressOne;
        }
        [When(@"I click (.*)")]
        public void WhenIclick(string line)
        {
            component.Find(line).Click();
        }
        [Then(@"the result should be displayed")]
        public void theResltshouldbedisplayed()
        {
            component.Find("display").Should().NotBeNull();
        }
        [Then(@"an error should be displayed showing where i made a mistake")]
        public void AnErrorShouldBeDisplayed()
        {
            component.Find("Error").Should().NotBeNull();
        }
    }
}
