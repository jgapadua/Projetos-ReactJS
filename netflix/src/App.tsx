import { useEffect, useState } from "react";
import MovieRow from "./components/MovieRow";
import Tmdb, { ICategory } from "./Tmdb";
import "./App.css";
import FeatureMovie from "./components/FeatureMovie";
import Header from "./components/Header";
import Footer from "./components/Footer";

const App = () => {
  const [movieList, setMovieList] = useState<ICategory[]>([]);
  const [featureData, setFeatureData] = useState<ICategory | any>(null);
  const [blackHeader, setBlackHeader] = useState(false);

  useEffect(() => {
    const loadAll = async () => {
      //pegando a lista
      let list = await Tmdb.getHomeList();
      setMovieList(list);

      //pegando o destaque
      let originals = list.filter((item) => item.slug === "originals");
      let randomMovie = Math.floor(
        Math.random() * originals[0].items.results.length - 1
      );
      let chosen = originals[0].items.results[randomMovie];

      let chosenInfo = await Tmdb.getMovieInfo(chosen.id, "tv");
      setFeatureData(chosenInfo);
    };
    loadAll();
  }, []);
  
  //adicionando evento de scroll
  useEffect(() => {
    const scrollListiner = () => {
      if (window.scrollY > 10) {
        setBlackHeader(true);
      } else {
        setBlackHeader(false);
      }
    };
    window.addEventListener("scroll", scrollListiner);
    return () => {
      window.removeEventListener("scroll", scrollListiner);
    };
  }, []);

  return (
    <div className="page">
      <Header black={blackHeader}></Header>

      {featureData && <FeatureMovie item={featureData} />}
      {/**
       * trailer && <ReactPlayer url = {trailer}/>
       */}
      <section className="lists">
        {movieList.map((item, key) => (
          <MovieRow key={key} title={item.title} items={item.items} />
        ))}
      </section>
      
      <Footer></Footer>

      {movieList.length <= 0 && (
        <div className="loading">
          <img
            src="https://media.filmelier.com/noticias/br/2020/03/Netflix_LoadTime.gif"
            alt="Carregando"
          />
        </div>
      )}
    </div>
  );
};
export default App;
