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
    public class CompareStepDefinition
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
        [Given(@"i have found the address (.*)")]
        public void GivenIHaveTheAddress(string address)
        {
            throw new NotImplementedException();
        }
        [Given(@"I Have found second adress (.*)")]
        public void GivenIHaveTheSecondAddress(string address)
        {
            throw new NotImplementedException();
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
       

    }
}
