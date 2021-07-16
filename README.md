# Reposit�rio Oficial do Projeto MedicalManagement-Sys

## Este projeto � o Caso de Uso do nosso Curso de Asp.NetCore MVC - Do Zero ao Ninja

### DataCadastro:

> Todo: O campo DataCadastro deve ser inclu�do automaticamente, mas n�o deve ser alterado. Este processo ser� implementado no contexto da aplica��o.

---

> ### TaskList - Tag Helpers

 Feature														| Complexidade	| Status	
---------------------------------------------------------------	| -------------	| --------	
 Criar uma Tag Helper para exibir um e-mail no Rodap�			| Alta			| Ok		
 Podemos usar um dom�nio padr�o ou um parametrizado				| Alta			| Ok		
 Vamos customizar a Tag Html "a", usando, inclusive, o mailTo	| Normal		| Not Implem		
 Preciso de uma classe com o sufixo Tagelper					| Baixa			| Not Implem
 Preciso que a classe acima Herde de TagHelper					| Alta			| Not Implem
 Package: using Microsoft.Asp.NetCore.Razor.TagHelpers			| Baixa			| Not Implem
 Essa class precisa sobrescrever a Task ProcessAsync			| M�dia			| Not Implem
 Par�metros: (TagHelperContext context, TagHelperOutput output)	| M�dia			| Not Implem
 Neste exemplo vamos deixar o context de lado					| Baixa			| Ok
 Usaremos um dom�nio default ou receberemos no par�metro		| Normal		| OK
 Usaremos a nota��o Kebab Case: MeuEmail  => meu-email			| Baixa			| Ok

---

### C�digo pronto do nosso EmailTagHelper

```CSharp
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cooperchip.ITDeveloper.Mvc.Extentions.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string Domain { get; set; } = "eaditdeveloper.com.br";
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var content = await output.GetChildContentAsync();
            var target = content.GetContent() + "@" + Domain;
            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(target);
        }
    }
}
```

### Migrando nossa aplica��o do Asp.Net Core 2.2 para 3.0 / 3.1.


>> Se voc� for um iniciante em Asp.Net Core talvez nunca tenha ouvido falar de AddMvcCore().



Consultar a documenta��o para TagHelpers e ViewComponents, **[Leia aqui](https://docs.microsoft.com/pt-br/)**.
Consultar a documenta��o para MarkDown, **[Leia aqui](http://daringfireball.net/projects/markdown/basics)**.