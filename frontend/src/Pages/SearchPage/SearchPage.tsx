import React, { useState, ChangeEvent, SyntheticEvent } from "react";
import Navbar from "../../Components/Navbar/Navbar";
import { CompanySearch } from "../../company";
import { searchCompanies } from "../../api";
import Search from "../../Components/Search/Search";
import ListPortfolio from "../../Components/Portfolio/ListPortfolio/ListPortfolio";
import CardList from "../../Components/CardList/CardList";
import { PortfolioGet } from "../../Models/Portfolio";

interface Props {}

const SearchPage = (props: Props) => {
  const [search, setSearch] = useState<string>("");
  const [portfolioValues, setPortfolioValues] = useState<PortfolioGet[]>([]);
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const onPortfolioCreate = (e: any) => {
    e.preventDefault();
    const newStock: PortfolioGet = {
      id: Date.now(), // temporary ID
      symbol: e.target[0].value,
      companyName: "", // These would typically come from your API
      purchase: 0,
      lastDiv: 0,
      industry: "",
      marketCap: 0,
      comments: []
    };
    
    const exists = portfolioValues.find((value) => value.symbol === newStock.symbol);
    if (exists) return;
    
    const updatedPortfolio = [...portfolioValues, newStock];
    setPortfolioValues(updatedPortfolio);
  };

  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    const symbolToDelete = e.target[0].value;
    const removed = portfolioValues.filter((value) => {
      return value.symbol !== symbolToDelete;
    });
    setPortfolioValues(removed);
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const result = await searchCompanies(search);
    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
    }
  };

  return (
    <>
      <Search
        onSearchSubmit={onSearchSubmit}
        search={search}
        handleSearchChange={handleSearchChange}
      />
      <ListPortfolio
        portfolioValues={portfolioValues}
        onPortfolioDelete={onPortfolioDelete}
      />
      <CardList
        searchResults={searchResult}
        onPortfolioCreate={onPortfolioCreate}
      />
      {serverError && <div>Unable to connect to API</div>}
    </>
  );
};

export default SearchPage;