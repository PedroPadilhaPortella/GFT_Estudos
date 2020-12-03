using System.Collections.Generic;

namespace api.HATEOAS
{
    public class HATEOAS
    {
        private string url;
        private string protocol = "https://";
        public List<Link> actions = new List<Link>();

        public HATEOAS(string url) {
            this.url = url;
        }
        public HATEOAS(string url, string protocol) {
            this.url = url;
            this.protocol = protocol;
        }

        public void AddAction(string rel, string method) {
            actions.Add(new Link(this.protocol + this.url, rel ,method));
        }

        public Link[] GetActions(string sufixo){
            //Clonagem dos elementos
            Link[] Links = new Link[actions.Count];
            for(int i = 0; i < Links.Length; i++){
                Links[i] = new Link(actions[i].href, actions[i].rel, actions[i].method);
            }

            // Montagem do Link
            foreach(var link in Links){
                link.href = link.href + "/" + sufixo;
            }

            return Links;
        }
    }
}