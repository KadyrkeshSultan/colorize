namespace Colorize
{
    public class ClientService
    {
        public Algorithmia.Client client;

        public ClientService()
        {
            client = new Algorithmia.Client("sim+J7DtqBc23Imjz9t2k22tsJ21");
        }

        public Algorithmia.Client GetClient()
        {
            return client;
        }
    }
}
