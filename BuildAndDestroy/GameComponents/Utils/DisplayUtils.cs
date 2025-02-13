using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.Utils
{
    /// <summary>
    /// Données et fonction utile d'affichage accesible par toutes les autres classe // Singleton
    /// </summary>
    public class DisplayUtils
    {
        /// <summary>
        /// instance du singleton
        /// </summary>
        private static DisplayUtils instance;

        /// <summary>
        /// Permet d'accedé au ressource
        /// </summary>
        private ContentManager content;

        private DisplayUtils() { }
        /// <summary>
        /// Texture blanche de 1 par 1
        /// </summary>
        public Texture2D blank { get; set; }

        /// <summary>
        /// Police par default
        /// </summary>
        public SpriteFont defaultFont { get; set; }

        public int width { get; set; }
        public int height { get; set; }


        /// <summary>
        /// Obtenir l'instance du Singleton
        /// </summary>
        /// <returns>l'instance</returns>
        public static DisplayUtils GetInstance()
        {
            if (instance == null)
            {
                instance = new DisplayUtils();
            }
            return instance;
        }

        /// <summary>
        /// Obtiens une ressource depuis son répertoire
        /// </summary>
        /// <typeparam name="T">Type de la ressource</typeparam>
        /// <param name="path">chemain d'accès</param>
        /// <returns>la valeur du type demander au chemain indiquer</returns>
        public T GetByPath<T>(string path)
        {
            return content.LoadLocalized<T>(path);
        }

        /// <summary>
        /// Ne peut être appeler qu'une fois // Permet d'assigner un répertoire avec les ressources
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(ContentManager content)
        {
            if (this.content == null)
            {
                this.content = content;
            }
        }
    }
}
