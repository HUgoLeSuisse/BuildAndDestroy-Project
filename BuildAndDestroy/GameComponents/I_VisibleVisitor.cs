using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Weapon;
using BuildAndDestroy.GameComponents.UI.Element;
using BuildAndDestroy.GameComponents.Utils;

namespace BuildAndDestroy.GameComponents
{
    /// <summary>
    /// Peremet d'appeler une fonction différante en fonction du type spécifique appelé // Interface Visiteur
    /// </summary>
    public interface I_VisibleVisitor
    {
        public void Visit(I_Visible v);
        public void Visit(UI_Pannel v);
        public void Visit(UI_Label v);
        public void Visit(UI_Skill v);
        public void Visit(UI_GamePannel v);
        public void Visit(UI_Button v);
        public void Visit(UI_StatPannel v);
        public void Visit(E_Entity v);
        public void Visit(E_Player v);
        public void Visit(W_Weapon v);
        public void Visit(DrawableCircle v);

    }

}