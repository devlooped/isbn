![Icon](https://raw.githubusercontent.com/devlooped/isbn/main/assets/img/icon-32.png) ISBN parsing library
============

[![Version](https://img.shields.io/nuget/vpre/ISBN.svg?color=royalblue)](https://www.nuget.org/packages/ISBN)
[![Downloads](https://img.shields.io/nuget/dt/ISBN.svg?color=green)](https://www.nuget.org/packages/ISBN)
[![License](https://img.shields.io/github/license/devlooped/isbn.svg?color=blue)](https://github.com/devlooped/isbn/blob/main/license.txt)

<!-- #content -->
An ISBN parsing library

# Usage

```csharp
if (ISBN.TryParse("9780753557525", out var isbn))
    // Renders: Publisher: 7535, Article: 5752 (Group 0, English language)
    Console.WriteLine($"Publisher: {isbn.Publisher}, Article: {isbn.Article} (Group {isbn.Group}, {isbn.GroupName})");
```

## Attribution

The implementation is mostly a port from https://github.com/inventaire/isbn3, and consumes the 
group information published in that repository.

<!-- #content -->
<!-- include https://github.com/devlooped/sponsors/raw/main/footer.md -->
# Sponsors 

<!-- sponsors.md -->
[![Clarius Org](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/clarius.png "Clarius Org")](https://github.com/clarius)
[![Christian Findlay](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MelbourneDeveloper.png "Christian Findlay")](https://github.com/MelbourneDeveloper)
[![C. Augusto Proiete](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/augustoproiete.png "C. Augusto Proiete")](https://github.com/augustoproiete)
[![Kirill Osenkov](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KirillOsenkov.png "Kirill Osenkov")](https://github.com/KirillOsenkov)
[![MFB Technologies, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MFB-Technologies-Inc.png "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)
[![Amazon Web Services](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/aws.png "Amazon Web Services")](https://github.com/aws)
[![SandRock](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/sandrock.png "SandRock")](https://github.com/sandrock)
[![David Pallmann](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/davidpallmann.png "David Pallmann")](https://github.com/davidpallmann)


<!-- sponsors.md -->

[![Sponsor this project](https://raw.githubusercontent.com/devlooped/sponsors/main/sponsor.png "Sponsor this project")](https://github.com/sponsors/devlooped)
&nbsp;

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- https://github.com/devlooped/sponsors/raw/main/footer.md -->
