export interface AccessTokenResponse {
    tokenType: string;
    accessToken: string;
    refreshToken: string;
    expiresIn: number; 
    user: AccessUser;
}

export interface AccessUser {
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    previousKey?: string; 
    currentKey?: string;  
    userRole: string;
    userGroup: string;
}