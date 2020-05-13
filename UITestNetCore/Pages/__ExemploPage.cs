using UITestNetCore.Bases;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITestNetCore.Pages
{
    public class __ExemploPage : PageBase
    {
        #region Mapping
        By txtCpf = By.Id("cpf");
        By txtSenha = By.Id("password");
        By btnAcessar = By.XPath("//button[text()='Acessar']");//button[text()='Avançar']  

        By btnPesquisar = By.Name("btnK");
        By txtPesquisar = By.Name("q");

        
        #endregion

        #region Actions
        public void EscreverCPF(string cpf)
        {
            SendKeys(txtCpf, cpf);
        }

        public void EscreverSenha(string senha)
        {
            SendKeys(txtSenha, senha);
        }

        public void ClicarBotaoAcessar()
        {
            Click(btnAcessar);
        }


        public void EscreverTxtPesquisar(string texto)
        {
            SendKeys(txtPesquisar, texto);
        }

        public void ClicarBtnPesquisar()
        {
            Click(btnPesquisar);
        }
        #endregion
    }
}
