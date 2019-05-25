using System.Drawing;

namespace EntityWorld
{
    public class WorldCreationParameters
    {
        /// <summary>
        /// The metadata of the existing entities that should be brought forward into this world.
        /// </summary>
        public EntityMetadata[] ExistingEntities { get; set; }

        /// <summary>
        /// The size of the world
        /// </summary>
        public Size WorldSize { get; set; } = new Size(800, 600);

        /// <summary>
        /// The total number of entities (including existing entities) that should be created.
        /// </summary>
        public int NumberOfEntities { get; set; } = 1000;

        /// <summary>
        /// The number of instructions each generated entity should contain.
        /// </summary>
        public int NumberOfInstructions { get; set; } = 20;

        /// <summary>
        /// The maximum amount of food each entity can hold.
        /// </summary>
        public int MaxFood { get; set; } = 1000;

        /// <summary>
        /// The size of the food area.
        /// </summary>
        public Size FoodSize { get; set; } = new Size(20, 20);
    }
}