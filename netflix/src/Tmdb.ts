const API_KEY = "a5d899ae2e7c8b0b3537072b2e368722";
const API_BASE_URL = "https://api.themoviedb.org/3";

/**
 * - originais da netflix
 * - recomendados (trending)
 * - em alta (top rated)
 * - ação
 * - comédia
 * - terror
 * - romance
 * - documentários
 *
 */

const basicFetch = async (endpoint: any) => {
  try {
    const response = await fetch(`${API_BASE_URL}${endpoint}`);
    return await response.json();
  } catch (error) {
    console.log("Error basicFetch" + error);
  }
};

export interface IJsonAttribute{
  original_name:string;
  backdrop_path:string;
  vote_average:number;
}

type Attribute = {
  [results: string]: any;
  movieId: string | number;
};

export interface ICategory {
  slug?: string;
  title: string;
  items: Attribute;

}

export default {
  getHomeList: async (): Promise<ICategory[]> => {
    return [
      {
        slug: "originals",
        title: "Originais do Netflix",
        items: await basicFetch(
          `/discover/tv?api_key=${API_KEY}&language=pt-BR&with_networks=213`
        ),
      },
      {
        slug: "trending",
        title: "Recomendados para Você",
        items: await basicFetch(
          `/trending/all/week?api_key=${API_KEY}&language=pt-BR`
        ),
      },
      {
        slug: "toprated",
        title: "Em Alta",
        items: await basicFetch(
          `/movie/top_rated?api_key=${API_KEY}&language=pt-BR`
        ),
      },
      {
        slug: "action",
        title: "Ação",
        items: await basicFetch(
          `/discover/movie?api_key=${API_KEY}&language=pt-br&with_genres=28`
        ),
      },
      {
        slug: "comedy",
        title: "Comédia",
        items: await basicFetch(
          `/discover/movie?api_key=${API_KEY}&language=pt-br&with_genres=35`
        ),
      },
      {
        slug: "horror",
        title: "Terror",
        items: await basicFetch(
          `/discover/movie?api_key=${API_KEY}&language=pt-br&with_genres=27`
        ),
      },
      {
        slug: "romace",
        title: "Romance",
        items: await basicFetch(
          `/discover/movie?api_key=${API_KEY}&language=pt-br&with_genres=10749`
        ),
      },
      {
        slug: "documentary",
        title: " Documentários",
        items: await basicFetch(
          `/discover/movie?api_key=${API_KEY}&language=pt-br&with_genres=99`
        ),
      },
    ];
  },

  getMovieInfo: async (movieId: Attribute, type: any) => {
    let info = {};
    if (movieId) {
      switch (type) {
        case "movie":
          info = await basicFetch(
            `/movies/${movieId}?language=pt-BR&api_key=${API_KEY}`
          );
          break;
        case "tv":
          info = await basicFetch(
            `/tv/${movieId}?language=pt-BR&api_key=${API_KEY}`
          );
          break;
      }
    }
    return info;
  },
};
