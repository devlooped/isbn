![Icon](https://raw.githubusercontent.com/devlooped/isbn/main/assets/img/icon-32.png) ISBN parsing library
============

An ISBN parsing library

[![Version](https://img.shields.io/nuget/vpre/ISBN.svg?color=royalblue)](https://www.nuget.org/packages/ISBN)
[![Downloads](https://img.shields.io/nuget/dt/ISBN.svg?color=green)](https://www.nuget.org/packages/ISBN)
[![License](https://img.shields.io/github/license/devlooped/isbn.svg?color=blue)](https://github.com/devlooped/isbn/blob/main/license.txt)

# Usage

```csharp
if (ISBN.TryParse("9780753557525", out var isbn))
    // Renders: Publisher: 7535, Article: 5752 (Group 0, English language)
    Console.WriteLine($"Publisher: {isbn.Publisher}, Article: {isbn.Article} (Group {isbn.Group}, {isbn.GroupName})");
```

At the moment, supports the following CSS selector features: 

- [Type selector](https://www.w3.org/TR/selectors-3/#type-selectors)
- [Universal selector](https://www.w3.org/TR/selectors-3/#universal-selector)
- [Attribute selector](https://www.w3.org/TR/selectors-3/#attribute-selectors)
- [Class selector](https://www.w3.org/TR/selectors-3/#class-html)
- [ID selector](https://www.w3.org/TR/selectors-3/#id-selectors)
- [Pseudo-classes](https://www.w3.org/TR/selectors-3/#pseudo-classes):
    * [:checked](https://www.w3.org/TR/selectors-3/#checked)
    * [:first-child](https://www.w3.org/TR/selectors-3/#first-child-pseudo)
    * [:last-child](https://www.w3.org/TR/selectors-3/#last-child-pseudo)
    * [:only-child](https://www.w3.org/TR/selectors-3/#only-child-pseudo)
    * [:empty](https://www.w3.org/TR/selectors-3/#empty-pseudo)
    * [:first-of-type](https://www.w3.org/TR/selectors-3/#first-of-type-pseudo)
    * [:last-of-type](https://www.w3.org/TR/selectors-3/#last-of-type-pseudo)
    * [:not(...)](https://www.w3.org/TR/selectors-3/#negation)
    * [:nth-of-type(n)](https://www.w3.org/TR/selectors-3/#nth-of-type-pseudo)
    * [:has(...)](https://www.w3.org/TR/selectors-4/#has-pseudo)

And all [combinators](https://www.w3.org/TR/selectors-3/#combinators)

Non-CSS features:

- `text()` pseudo-attribute selector: selects the node text contents, as specified 
  in the [XPath](https://www.w3.org/TR/1999/REC-xpath-19991116/) `text()` location 
  path. Can be used instead of an attribute name selector, such as `div[text()=foo]`. 
  All [attribute value selectors](https://www.w3.org/TR/selectors-3/#attribute-selectors) 
  are also supported, such as `div[text()$=suffix]` or `div[text()*=yay]`


# Dogfooding

[![CI Version](https://img.shields.io/endpoint?url=https://shields.kzu.io/vpre/Devlooped.Xml.Css/main&label=nuget.ci&color=brightgreen)](https://pkg.kzu.io/index.json)
[![Build](https://github.com/devlooped/css/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/css/actions)

We also produce CI packages from branches and pull requests so you can dogfood builds as quickly as they are produced. 

The CI feed is `https://pkg.kzu.io/index.json`. 

The versioning scheme for packages is:

- PR builds: *42.42.42-pr*`[NUMBER]`
- Branch builds: *42.42.42-*`[BRANCH]`.`[COMMITS]`



## Sponsors

[![sponsored](https://raw.githubusercontent.com/devlooped/oss/main/assets/images/sponsors.svg)](https://github.com/sponsors/devlooped) [![clarius](https://raw.githubusercontent.com/clarius/branding/main/logo/byclarius.svg)](https://github.com/clarius)[![clarius](https://raw.githubusercontent.com/clarius/branding/main/logo/logo.svg)](https://github.com/clarius)

*[get mentioned here too](https://github.com/sponsors/devlooped)!*
