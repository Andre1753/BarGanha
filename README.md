# 🎈 Bar.Ganha - Ecommerce das trocas
### Projeto de TCC distribuido e aprovado.

<div align="left">
  <h3>🔄 Ecossistema de Economia Circular e Trocas Diretas</h3>
  <p><i>Projeto de Conclusão de Curso (TCC) focado em escalabilidade e arquitetura limpa.</i></p>
</div>

<div align="center">
  <img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Azure-0089D6?style=for-the-badge&logo=microsoft-azure&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" />
</div>

---

## 🏗️ Engenharia e Organização (Clean Code)
O projeto foi estruturado utilizando padrões de **Arquitetura em Camadas** para garantir o desacoplamento e a manutenibilidade do sistema:

* **BarGanha.BLL (Business Logic Layer)**: Camada responsável por toda a lógica de negócio e regras de validação de trocas.
* **BarGanha.DAL (Data Access Layer)**: Camada de persistência isolada, gerenciando a comunicação com o SQL Server.
* **Pattern MVC**: Implementação em ASP.NET 3.0 para separação clara entre visão, controle e modelo de dados.

## ☁️ Infraestrutura e Cloud
* **Microsoft Azure**: Hospedagem da aplicação e do banco de dados, validando o ciclo completo de deploy em nuvem.
* **SQL Server**: Modelagem relacional para suporte a transações complexas de anúncios e negociações.

## 🚀 Visão de Produto
Plataforma desenvolvida para permitir a troca direta de itens entre usuários. O roadmap estratégico incluía a implementação de uma **moeda social** interna e a expansão para troca de serviços, visando um sistema financeiro comunitário completo.

---

## 🛠️ Como Executar
1. Clone o repositório.
2. Configure a connection string no arquivo de configuração para apontar para sua instância de SQL Server.
3. Certifique-se de ter o SDK do .NET 3.0 instalado.
4. Execute `dotnet run`.

---
<p align="center">
  Projeto acadêmico com desenvolvido por <b>André Araújo Silva</b>.
</p>
