using UITestNetCore.Pages;

namespace UITestNetCore.Flows
{
    public class __ExemploFlows
    {
        #region Page Object and Constructor
        __ExemploPage exemploPage;

        public __ExemploFlows()
        {
            exemploPage = new __ExemploPage();
        }
        #endregion

        public void Pesquisar(string texto)
        {
            exemploPage.EscreverTxtPesquisar(texto);
            exemploPage.ClicarBtnPesquisar();
        }
    }
}
