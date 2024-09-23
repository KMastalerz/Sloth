export interface LoginCommand {
    login: string;
    password: string;
}
export interface RefreshTokenCommand {
    userName: string;
    refreshToken: string;
}