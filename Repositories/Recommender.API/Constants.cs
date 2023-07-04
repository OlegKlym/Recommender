namespace Recommender.API
{
    public static class Constants
    {
        public static string BASE_URL = "http://www.recommender.somee.com/api/";

        public static string AUTH_LOGIN_URL = "users/sign_in";
        public static string AUTH_SIGNUP_URL = "users/sign_up";

        public static string GET_RECOMMENDATIONS_URL = "recommendations/get_my_recommendations";
        public static string SEARCH_MOVIES_URL = "movies/get_all_movies";
        public static string RATE_MOVIES_URL = "rates/add_movies_rates";
    }
}
