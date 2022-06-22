import AutoComplete from './AutoComplete'


describe('AutoComplete.cy.tsx', () => {
    it('playground', () => {
        cy.mount(<AutoComplete />)
    })
})

//Cypress.on('uncaught:exception', (err, runnable) => {
//    console.log(err);
//    return false;
//})