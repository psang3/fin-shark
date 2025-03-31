import axios from 'axios';

const api = axios.create({
    baseURL: 'https://financialmodelingprep.com/api/v3'
});

const API_KEY = 'YOUR_API_KEY';

export interface CompanySearch {
    symbol: string;
    name: string;
    currency: string;
    stockExchange: string;
    exchangeShortName: string;
}

export interface Stock {
    symbol: string;
    price: number;
    beta: number;
    volAvg: number;
    mktCap: number;
    lastDiv: number;
    range: string;
    changes: number;
    companyName: string;
    currency: string;
    isin: string;
    exchange: string;
    exchangeShortName: string;
    industry: string;
    website: string;
    description: string;
    ceo: string;
    sector: string;
    country: string;
    fullTimeEmployees: string;
    phone: string;
    address: string;
    city: string;
    state: string;
    zip: string;
    dcfDiff: number;
    dcf: number;
    image: string;
    ipoDate: string;
}

export const searchStocks = async (query: string): Promise<CompanySearch[]> => {
    try {
        const response = await api.get(`/search?query=${query}&limit=10&apikey=${API_KEY}`);
        return response.data;
    } catch (error) {
        console.error('Error searching stocks:', error);
        return [];
    }
};

export const getStockDetails = async (symbol: string): Promise<Stock | null> => {
    try {
        const response = await api.get(`/profile/${symbol}?apikey=${API_KEY}`);
        return response.data[0] || null;
    } catch (error) {
        console.error('Error fetching stock details:', error);
        return null;
    }
}; 