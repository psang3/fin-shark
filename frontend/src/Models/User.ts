export interface UserProfile {
    userName: string;
    email: string;
}

export interface UserProfileToken extends UserProfile {
    token: string;
}

export interface LoginRequest {
    userName: string;
    password: string;
}

export interface RegisterRequest {
    email: string;
    userName: string;
    password: string;
}

export interface PortfolioPost {
    symbol: string;
    companyName: string;
    shares: number;
    purchasePrice: number;
    purchaseDate: string;
}

export interface PortfolioGet {
    id: number;
    symbol: string;
    companyName: string;
    shares: number;
    purchasePrice: number;
    purchaseDate: string;
}