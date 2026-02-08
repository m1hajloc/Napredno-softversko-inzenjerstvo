import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { App } from './app/app';
import { apiKeyInterceptor } from './app/api-key-interceptor';

bootstrapApplication(App, {
  providers: [provideHttpClient(withInterceptors([apiKeyInterceptor]))],
}).catch((err) => console.error(err));
