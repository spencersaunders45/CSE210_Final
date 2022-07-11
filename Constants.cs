using System;

namespace CSE210_Final
{
   public class Constants
   {
      // GAME
      public static string GAME_NAME = "Save your Dog!";
      public static int FRAME_RATE = 60;

      // SKELETON SPAWN LOCATIONS
      private static int[] location1 = {0, 10};
      private static int[] location2 = {25, 30};
      private static int[] location3 = {45, 50};
      private static int[] location4 = {65 ,70};
      private static int[] location5 = {85, 90};
      private static int[] location6 = {105, 110};
      private static int[] location7 = {125, 130};
      private static int[] location8 = {145, 150};
      public static int[][] SEKELETON_LOCATIONS = {location1, location2, location3, location4, location5, location6, location7, location8,};

      // SCREEN
      public static int SCREEN_WIDTH = 1040;
      public static int SCREEN_HEIGHT = 680;
      public static int CENTER_X = SCREEN_WIDTH / 2;
      public static int CENTER_Y = SCREEN_HEIGHT / 2;

      // FIELD
      public static int FIELD_TOP = 60;
      public static int FIELD_BOTTOM = SCREEN_HEIGHT;
      public static int FIELD_LEFT = 0;
      public static int FIELD_RIGHT = SCREEN_WIDTH;

      // FONT

      // SOUND

      // TEXT

      // COLORS

      // KEYS

      // MAP

   }
}