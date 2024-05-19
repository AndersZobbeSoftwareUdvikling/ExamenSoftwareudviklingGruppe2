using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Bunit;
using FluentAssertions;
using Forbrugsenhed_Sammenligner_gruppe_2;
using Forbrugsenhed_Sammenligner_gruppe_2.Components.Pages;

namespace Forbrugsenhed_Sammenligner_Test
{
    public class Frontendtests
    {
        [Fact]
        public void Adresse1SearchButtonSearches()
        {
            //Arrange
            using var ctx = new TestContext();
            IRenderedComponent<BFESammenlignerPage> component = ctx.RenderComponent<BFESammenlignerPage>();
            testParser testParser = new testParser();
            component.Instance.addressParser = testParser;


            //Act
            
            component.Find("[name=ButtonAdresse1]").Click();

            //Assert
            testParser.IHaveBenUsed.Should().BeTrue();
            
        }
        
        [Fact]
        public void Adresse2SearchButtonSearches()
        {
            //Arrange
            using var ctx = new TestContext();
            IRenderedComponent<BFESammenlignerPage> component = ctx.RenderComponent<BFESammenlignerPage>();
            testParser testParser = new testParser();
            component.Instance.addressParser = testParser;

            //Act

            component.Find("[name=ButtonAdresse1]").Click();

            //Assert
            testParser.IHaveBenUsed.Should().BeTrue();

        }
        
        [Fact]
        public void Adresse1SearchButtonSearchesForTheTextInBox1()
        {
            //Arrange
            using var ctx = new TestContext();
            IRenderedComponent<BFESammenlignerPage> component = ctx.RenderComponent<BFESammenlignerPage>();
            testParser testParser = new testParser();
            component.Instance.addressParser = testParser;

            //Act
            component.Find("[name=TextAdresse1]").Input<String>("TestAdress1");
            component.Find("[name=ButtonAdresse1]").Click();

            //Assert
            testParser.ParsedString.Should().Be("TestAdress1");

        }

        [Fact]
        public void Adresse2SearchButtonSearchesForTheTextInBox2()
        {
            //Arrange
            using var ctx = new TestContext();
            IRenderedComponent<BFESammenlignerPage> component = ctx.RenderComponent<BFESammenlignerPage>();
            testParser testParser = new testParser();
            component.Instance.addressParser = testParser;

            //Act
            component.Find("[name=TextAdresse2]").Input<String>("TestAdress2");
            component.Find("[name=ButtonAdresse2]").Click();

            //Assert
            testParser.ParsedString.Should().Be("TestAdress2");

        }

        [Fact]
        public void ClearAdresse1ButtonClearsAdress()
        {
            //Arrange
            using var ctx = new TestContext();
            IRenderedComponent<BFESammenlignerPage> component = ctx.RenderComponent<BFESammenlignerPage>();
            component.Instance.Adress1Output = "Test";

            //Act
            component.Find("[name=ClearAdress1]").Click();

            //Assert
            component.Find("[name=Adress1Output]").TextContent.Should().Be("");
        }
        [Fact]
        public void ClearAdresse2ButtonClearsAdress()
        {
            //Arrange
            using var ctx = new TestContext();
            IRenderedComponent<BFESammenlignerPage> component = ctx.RenderComponent<BFESammenlignerPage>();
            component.Instance.Adress1Output = "Test";

            //Act
            component.Find("[name=ClearAdress2]").Click();

            //Assert
            component.Find("[name=Adress2Output]").TextContent.Should().Be("");
        }
        [Fact]
        public void CompareAdressesRunsComparison()
        {
            //Arrange
            using var ctx = new TestContext();
            IRenderedComponent<BFESammenlignerPage> component = ctx.RenderComponent<BFESammenlignerPage>();
            testComparer testComparer = new testComparer();
            component.Instance.addressCompare = testComparer;


            //Act

            component.Find("[name=CompareButton]").Click();

            //Assert
            testComparer.IHaveBenUsed.Should().BeTrue();
        }

    }

    public class testParser : IParser
    {
        
        public bool IHaveBenUsed = false;
        public string ParsedString = null;
        public testParser() { }

        public object Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        

        public void Parse(Dictionary<string, string> adress)
        {
            throw new NotImplementedException();
        }

        Dictionary<string, string> IParser.Parse(Dictionary<string, string> adress)
        {
            IHaveBenUsed = true;
            return adress;
        }
    }
    public class testComparer : IAddressCompare
    {

        public bool IHaveBenUsed = false;
        public string ParsedString = null;

        public Dictionary<string, string> result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Compare(Dictionary<string, object> AdressOne, Dictionary<string, object> AdressTwo)
        {
            IHaveBenUsed = true;

        }

        public void Compare(Dictionary<string, string> AdressOne, Dictionary<string, string> AdressTwo)
        {
            throw new NotImplementedException();
        }
    }
}
