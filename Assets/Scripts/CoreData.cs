using System.Collections.Generic;
using UnityEngine;

public class CoreData : MonoBehaviour
{
    // This is a dictionary, dateventures. It holds keys : Date Idea Header, and values : Date Idea Descriptions  
    Dictionary<string, string> dateventures = new Dictionary<string, string>
    {
        { "Dinner Diversions", "Craft a unique dinner by picking up each part from different places—drinks, apps, and main course. A culinary adventure awaits!" },
        { "Pillow Fort", "Build a cozy haven with blankets, pillows, and twinkling lights. Dive into a night of comfort, movies, and shared popcorn delights!" },
        { "Drive & Jive", "Embark on a carefree drive with a curated playlist. No directions, just tunes and the open road—creating memories mile by mile." },
        { "Slumber Soiree", "Indulge in a high-effort slumber party with warm pajamas, fresh blankets, face masks, and a tasty snack run. A movie marathon awaits!" },
        { "Cook Together", "Discover a delicious recipe online, gather ingredients, and create a culinary masterpiece side by side. Bonding through shared kitchen adventures!" },
        { "ChatGPT Surprise", "Challenge: Ask ChatGPT for a spontaneous date idea. If reasonable, embark on the mystery date for unexpected surprises and shared joy!" },
        { "Imaginary Feast", "Wing a recipe with kitchen ingredients and imagination. Create a delightful surprise dish together. Unleash your culinary creativity!" },
        { "Wine & Dine", "Savor a fancy wine night with a charcuterie board. Explore different wines and indulge in delectable pairings for a sophisticated, romantic evening." },
        { "Surprise Takeout", "Order unique takeout dishes for each other secretly. Revel in the surprise as you exchange and savor your mystery meals together." },
        { "Taste Testing", "Blindfolded taste testing challenge. Score each other's guesses for various flavors like BBQ sauce, ketchup, and soy sauce. A playful and tasty competition!" },
        { "Game Night", "Engage in a fun board game night. Challenge each other, strategize, and enjoy a playful competition. Quality time with a dash of friendly rivalry!" },
        { "Power Outage", "Simulate a fake power outage with candles, no lights or TV. Immerse in each other's company, creating an intimate and cozy atmosphere." },
        { "Spa Retreat", "Pamper yourselves with a spa night. Relax with face masks, soothing music, and a calming ambiance. Bonding through self-care and shared tranquility." },
        { "Campfire Magic", "Ignite the spark with a cozy campfire. Share stories, marshmallows, and the warmth of the fire. A night filled with warmth and connection." },
        { "Cat Cafe Adventure", "Visit a cat cafe. Enjoy the company of furry friends while sipping on your favorite beverages. A purr-fectly delightful date for cat lovers!" },
        { "Dress-Up Fun", "Get playful by dressing each other. Explore new styles, laugh at the results, and create lasting memories of your fashion escapade." },
        { "Hide n' Seek", "Compete in an extreme hide n' seek. Challenge each other's hiding skills in various locations. The thrill of the hunt awaits!" },
        { "Baking Battle", "Embark on a baking competition. Select ingredients and see who can create the most delicious treat. A sweet and playful showdown!" },
        { "Bowling Night", "Hit the lanes for a night of bowling. Enjoy the radio social atmosphere while aiming for strikes and creating shared memories." },
        { "Movie Theater", "Experience the magic of a movie theater night. Get your favorite snacks, pick a film, and enjoy the big screen ambiance together." },
        { "Arcade Adventures", "Head to an arcade for a fun-filled date. Play classic games, challenge each other, and revel in the nostalgia of arcade entertainment." },
        { "Couch Games", "Enjoy a night of couch games. From video games to board games, compete and collaborate in the comfort of your living room." },
        { "Blanket Creation", "Craft a cozy blanket together. Choose fabrics, colors, and patterns to create a unique and snuggly masterpiece for your shared moments." },
        { "Mini Golf", "Tee off on a mini-golf adventure. Navigate through fun obstacles and enjoy a lighthearted competition in a playful and vibrant setting." },
        { "Picnic Love", "Have a charming picnic, indoors or outdoors. Pack your favorite treats, find a cozy spot, and savor the moments of shared laughter and love." },
        { "Rowboat Romance", "Find a rowboat and paddle into tranquility. Enjoy the serenity of the water, creating beautiful memories in the peaceful embrace of nature." },
        { "Stargazing Bliss", "Lay back and gaze at the stars. Revel in the beauty of the night sky, sharing dreams and connecting under the celestial canopy." },
        { "Thrift Adventure", "Embark on a thrift store adventure. Pick outfits for each other, embracing unique styles and creating a fashion-forward date experience." },
        { "Skill Sharing", "Challenge yourselves with a new skill, like knitting. Learn together and create something special, celebrating the joy of shared accomplishments." },
        { "Cooking Class", "Take a cooking class together. Explore new cuisines, learn techniques, and bond over the shared experience of creating a delicious meal." },
        { "Pottery Passion", "Unleash your creativity in a pottery class. Mold and shape clay, creating unique pieces and memories that last a lifetime." },
        { "Starry Night", "Visit a planetarium. Explore the cosmos together, marveling at the stars and expanding your universe in a night of celestial wonder." },
        { "Fruitful Adventure", "Embark on a fruit-picking adventure. Harvest delicious fruits together, creating a connection with nature and enjoying the fruits of your labor." },
        { "Scavenger Hunt", "Engage in a thrilling scavenger hunt. Follow clues, solve puzzles, and share the excitement of discovery on a fun and adventurous quest." },
        { "Sunset Snuggles", "Wrap up the day with sunset snuggles. Bask in the warm glow, creating an intimate moment as the sun paints the sky." },
        { "Morning Sunrise", "Welcome the day with favorite beverages and a sunrise spectacle. Revel in the beauty of dawn and share quiet moments in each other's company." },
        { "Indoor Camping", "Create an indoor camping experience. Build a fort, tell stories, and enjoy the coziness of a camping adventure within the comfort of home." },
        { "Picture Perfect", "Embark on a photoshoot adventure. Capture moments, create memories, and have fun striking poses as you document your shared experiences." },
        { "Nature Painters", "Bob Ross-inspired painting. Create art together, laugh, and cherish the shared canvas memories in a nature-themed date." },
        { "Foot Race", "Pick a finish line and race there! Thrilling competition and shared joy in reaching the destination together." },
        { "Coin Drive Adventure", "Spontaneous road trip. Set time limits, flip coins at turns. Embrace the journey with excitement and spontaneity." },
        { "Holiday Fun", "Celebrate the current national holiday. Enjoy quirky activities and treats for a delightful, themed date." },
        { "Nerf Assassin", "Park nerf game. Hunt each other down, first to shoot the other wins! Exciting and playful competition in nature." },
        { "Shower Party", "Lively shower experience. Cool lights, speaker, crafted playlist—turn your shower into a unique party zone." },
        { "Sporty Day Out", "Play sports outside! Corn hole, disc golf, outdoor fun, friendly competition, and an active date filled with shared thrills." },
        { "Ice Cream Delight", "Sweet ice cream date. Explore flavors, create delicious memories, and enjoy the indulgence together." },
        { "Open House Visit", "Explore open houses. Imagine your dream home together, sharing visions and enjoying a playful adventure." },
        { "Hoodie Harmony", "Wear one oversized hoodie together. Cozy togetherness and warmth in shared clothing for the evening." },
        { "Wrestling Tournament", "Playful wrestling matches. Laugh, bond, and strengthen your connection through friendly physical competition." },
        { "Drive-In Movie Night", "Nostalgic drive-in movie. Cozy car bed, blankets, pillows—a night of cinematic magic under the stars." },
        { "Karaoke Adventure", "Karaoke bar fun. Sing your hearts out, revel in musical joy, and enjoy the lively karaoke atmosphere." },
        { "Dance Off Night", "Just Dance competition! Groove, showcase dance moves, and have an energetic night of fun together." },
        { "Board Game Discovery", "Find quirky games you haven't played before. Enjoy a fun-filled discovery night with laughter and strategy." },
        { "Massage Night", "Learn to massage. Unwind, connect, and share soothing touch for a night of relaxation and intimacy." },
        { "Bird Feeding", "Park bird feeding. Enjoy nature, observe birds, and create serene moments together in the great outdoors." },
        { "Playground Fun", "Find an empty playground! Embrace your inner child, swing, slide, and share laughter in a playful setting." },
        { "Laugh Challenge", "Try not to laugh. Watch funny videos together, enjoy shared laughter, and take on the challenge." },
        { "Teaching Moments", "Share skills. Take turns teaching each other something. Create moments of learning and connection in a fun way." },
        { "Themed Movie Night", "Immersive movie night. Pick a theme, set the lighting, and enjoy an atmospheric cinematic experience together." },
        { "Artistic Night", "Explore your artistic side with a painting night. Pass the canvas every 10 minutes, creating a collaborative masterpiece filled with shared creativity." },
        { "Early Hike", "Start the day with an early morning hike. Embrace nature, breathe in fresh air, and witness the sunrise together for an invigorating start." },
        { "Adventure Course", "Take on a high adventure course. Navigate treetop obstacles, overcoming challenges and forging a bond through shared thrills and triumphs." },
        { "Board Game Fusion", "Mash up rules from two board games. Create a unique gaming experience, combining elements for a one-of-a-kind night of strategic fun." },
        { "Yoga Together", "Deepen your connection with couples yoga. Stretch, balance, and support each other in a shared journey of relaxation and well-being." },
        { "Photo Album", "Start a photo album. Compile cherished moments and memories, creating a visual timeline of your journey together for a nostalgic and heartwarming keepsake." },
        { "Crossword Love", "Write crossword puzzles for each other. Share clues and solutions, adding a playful twist to a cozy night of brain-teasing fun." },
        { "Treasure Hunt", "Create a mini treasure hunt at home. Take turns hiding treasures and crafting clues, adding an element of surprise and adventure to your space." },
        { "Rock Reminders", "Visit a park or stream and find rocks that remind you of each other. Transform them into pet rocks, symbolizing your connection and love." },
        { "Cozy Childhood", "Get cozy and revisit childhood comfort shows. Snuggle up with your favorite snacks, enjoying the nostalgia of shows that shaped your younger years." },
        { "Flashback Games", "Relive childhood memories with old games. Play together, reminisce, and share the joy of retro gaming for a trip down memory lane." },
        { "Lock Screen Love", "Design phone lock screens for each other. Express your creativity and affection with personalized screens capturing moments and inside jokes." },
        { "Easter Egg Hunt", "Celebrate with an Easter egg hunt. Hide colorful eggs, hunt together, and share the joy of discovery in a festive and playful adventure." },
        { "Cold Case Fun", "Solve a fake cold case online. Collaborate to crack the mystery, adding a touch of intrigue and teamwork to your date night." },
        { "Chopped Challenge", "Experience a Chopped-style challenge at home. Select mystery ingredients for each other and create unique dishes in a playful culinary competition." },
        { "Holiday Creation", "Invent your own holiday. Create traditions, rituals, and celebrations unique to your relationship—something to look forward to and cherish every year." },
        { "Escape Room", "Navigate the challenges of an escape room. Work together to solve puzzles, test your teamwork, and experience the thrill of breaking free." },
        { "Petting Zoo Love", "Visit a petting zoo. Delight in the company of adorable animals, creating heartwarming moments in the midst of furry friends." },
        { "Tie-Dye Fun", "Get creative with tie-dyeing. Design vibrant and personalized clothing together, adding a splash of color to your shared memories." },
        { "Dance Class", "Take a dance class together. Learn new moves, laugh through missteps, and enjoy the rhythm of dancing in each other's arms." },
        { "Love List", "Create a list of your favorite things about each other. Share and discuss each point, deepening your connection through expressions of love and admiration." },
        { "Game Inventors", "Invent a new game and play it. Embrace creativity and laughter as you bring a unique and personalized game to life for a memorable experience." },
        { "Zoo Day", "Explore a zoo. Laugh, learn about animals, and create lively memories in the vibrant atmosphere." },
        { "Aquarium Visit", "Venture to an aquarium. Enjoy the marine wonders, share the magic, and experience an underwater adventure together." },
        { "Random Thrifting", "Thrift random clothes together, then venture out. Embrace unique finds and create a day full of surprises and shared experiences." },
        { "Graffiti Art", "Express creativity with graffiti. Create art, laugh, and enjoy a colorful date filled with self-expression and shared moments." },
        { "Show Binge", "Binge-watch a show. Snuggle up, share snacks, and immerse in the captivating world of your favorite, or a new series together." },
        { "Horror Car Night", "Car bed, horror movies, cozy blankets—a thrilling cinematic night under the stars in the back of your car." },
        { "YouTube Mysteries", "Explore unsolved mysteries on YouTube. Share intrigue, discuss theories, and enjoy a suspenseful night of entertainment." },
        { "Polaroid Memories", "Capture moments with Polaroid pics. Take snapshots throughout the day, creating a visual diary of shared experiences." },
        { "New Food Discovery", "Discover a new place for food or drinks. Expand culinary horizons and savor the excitement of trying something new together." },
        { "Persona Play Day", "Adopt new personas for the day. Have fun with characters, laugh, and experience the day from fresh and playful perspectives." },
        { "First Date Redux", "Pretend it's your first date again. Meet at a new location, relive the excitement, and create memorable moments together." },
        { "Rollerblading Fun", "Go rollerblading. Enjoy the thrill of gliding, laughter, and the shared joy of an active and fun-filled date." },
        { "Ice Skating Bliss", "Glide on ice together. Enjoy the beauty of ice skating, hold hands, and create delightful memories on the rink." },
        { "Swimming Getaway", "Go swimming at a fun location—beach or secret pond. Dive into the water, enjoy the atmosphere, and create refreshing memories." },
        { "Skinny Dip Adventure", "Embark on a skinny-dipping dare. Share a daring and intimate experience in a secluded and comfortable setting." },
        { "Expert Exploration", "Pretend to be experts. Examine something in your expert personas, share knowledge, and enjoy a playful day of exploration." },
        { "Museum Discovery", "Visit a museum. Immerse in art, history, or science, and share the wonder of discovery together." },
        { "Go Kart Thrills", "Experience go-karting thrills. Race each other, laugh, and create high-speed memories on the track." },
        { "Pedicure Pampering", "Get pedicures together. Relax, pamper yourselves, and enjoy a spa-like experience for a day of indulgent bonding." },
        // 100
        { "Adopt a Child", "Visit an adoption center and adopt a child in cash. Experience the joys and wonders of parenthood!" }
    };

    // dateventuresLength : Amount of dateventures in the dateventures dictionary
    // dateventureCounter : Number of dateventures completed
    // rollsThreshold : Represents the threshold for removing from availableRolls before adding back to it
    int dateventuresLength, dateventureCounter, rollsThreshold;

    // recentRolls : Track the rollsThreshold most recent rolls
    // availableRolls : The currently available date cards
    // dateKeys : All of the keys within dateventures
    List<string> recentRolls, availableRolls, dateKeys; 

    // Start is called before the first frame update
    void Start()
    {
        dateventuresLength = dateventures.Count;
        rollsThreshold = dateventuresLength / 4; // Set the threshold to a quarter of the size of dateventures
        dateKeys = new List<string>(dateventures.Keys);
        availableRolls = dateKeys;
        recentRolls = new List<string>();
    }

    // Getter for dateventures
    public Dictionary<string, string> Dateventures
    {
        get { return dateventures; }
    }

    // Getter and setter for recentRolls
    public List<string> RecentRolls
    {
        get { return recentRolls; }
        set { recentRolls = value; }
    }

    // Getter and setter for availableRolls
    public List<string> AvailableRolls
    {
        get { return availableRolls; }
        set { availableRolls = value; }
    }

    // Getter for dateKeys
    public List<string> DateKeys
    {
        get { return dateKeys; }
    }

    // Getter for dateventuresLength
    public int DateventuresLength
    {
        get { return dateventuresLength; }
    }

    // Getter for dateventureCounter
    public int DateventureCounter
    {
        get { return dateventureCounter; }
        set { dateventureCounter = value; }
    }

    // Getter for rollsThreshold
    public int RollsThreshold
    {
        get { return rollsThreshold; }
    }
}