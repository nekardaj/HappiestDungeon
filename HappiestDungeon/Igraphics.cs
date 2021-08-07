namespace HappiestDungeon
{
    public interface Igraphics
    {
        /// <summary>
        /// Presents its current state to the user, before calling this UpdateData should be called if anything changes
        /// </summary>
        void Render();
        /// <summary>
        /// Updates the data needed for rendering
        /// </summary>
        void UpdateData();
        
    }
}