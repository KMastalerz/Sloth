  // Storage type options
  export const enum StorageType {
    COOKIE,
    SESSION,
    LOCAL
  }

  export class CookieKeys {
    static readonly AuthToken = 'AuthToken';
    static readonly RefreshToken = 'RefreshToken';
  }