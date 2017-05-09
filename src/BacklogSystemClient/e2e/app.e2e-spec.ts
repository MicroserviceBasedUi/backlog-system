import { BacklogSystemClientPage } from './app.po';

describe('backlog-system-client App', () => {
  let page: BacklogSystemClientPage;

  beforeEach(() => {
    page = new BacklogSystemClientPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
