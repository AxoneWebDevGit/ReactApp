// Create a new file PageTitleContext.tsx

import React, { createContext, useContext, ReactNode, useState } from 'react';

interface PageTitleContextProps {
    pageTitle: string;
    setPageTitle: React.Dispatch<React.SetStateAction<string>>;
}

const PageTitleContext = createContext<PageTitleContextProps | undefined>(undefined);

export const PageTitleProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [pageTitle, setPageTitle] = useState<string>('Your Default Title');

    return (
        <PageTitleContext.Provider value={{ pageTitle, setPageTitle }}>
            {children}
        </PageTitleContext.Provider>
    );
};

export const usePageTitle = () => {
    const context = useContext(PageTitleContext);

    if (!context) {
        throw new Error('usePageTitle must be used within a PageTitleProvider');
    }

    return context;
};
